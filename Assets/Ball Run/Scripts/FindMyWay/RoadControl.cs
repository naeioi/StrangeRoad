using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadControl : MonoBehaviour
{
    public RoadUnit roadPrefab;
    public int firstRoads;

    // Elements to displayed on screen
    public List<RoadUnit> listRoadDisable;
    public List<RoadUnit> listRoadActive;
    public List<RoadUnit> listBonusActive;
    public List<BlockUnit> listBlock;
    public List<BlockUnit> listBlockBonus;

    public List<Vector3> listPointActive;
    public List<Vector3> listPointDisable;
    public List<bool> listType;

    public List<RoadUnit> activeRoads;
    public List<RoadUnit> idleRoads;
    public List<BlockUnit> activeBlocks;
    public List<BlockUnit> idleBlocks;

    int countDisable;

    public GameDefine.Direction turn;
    public PlayerControl player;

    void Awake()
    {
        player = MainObjControl.Instant.playerCtrl;
        RoadUnit rootRoad = ExtendPath(null, 1);
        player.currentRoad = rootRoad;
    }

    void CreateFirstRoad()
    {
        listPointActive.Add(new Vector3(0, 0, 0));
        listPointActive.Add(new Vector3(0, 0, 14));
       
        listType.Add(true);
        listType.Add(true);
        CreatePath(listPointActive[listPointActive.Count - 2], listPointActive[listPointActive.Count - 1], listType[listType.Count - 2], true);

        for (int i = 0; i < firstRoads; i++)
        {
            AddNewPoint(true);
        }
    }

    void AddNewPoint(bool isUp)
    {
        // Add a point to listPointActive
        // Do animation for road and block
        // Create new path and block path

        Vector3 newPath;
        switch (turn)
        {
            case GameDefine.Direction.Forward:
                newPath = new Vector3(0, 0, GameDefine.roadRandSize);
                break;
            case GameDefine.Direction.Left:
                newPath = new Vector3(-GameDefine.roadRandSize, 0, 0);
                break;
            default:
                newPath = new Vector3(GameDefine.roadRandSize, 0, 0);
                break;
        }
       

        listPointActive.Add(listPointActive[listPointActive.Count - 1] + newPath);
        listType.Add(true);
        Vector3 p1 = listPointActive[listPointActive.Count - 3];
        Vector3 p2 = listPointActive[listPointActive.Count - 2];
        Vector3 p3 = listPointActive[listPointActive.Count - 1];

        if (listRoadActive.Count > 2)
        {
            // Road
            Vector3 pos = listRoadActive[listRoadActive.Count - 1].transform.position;
            StartCoroutine(EffectControl.Move(listRoadActive[listRoadActive.Count - 1].gameObject, new Vector3(pos.x, 0, pos.z), 0.4f, null));

            // Wrong block platform
            pos = listBonusActive[listBonusActive.Count - 1].transform.position;
            StartCoroutine(EffectControl.Move(listBonusActive[listBonusActive.Count - 1].gameObject, new Vector3(pos.x, 0, pos.z), 0.4f, null));

            // Right block
            pos = listBlock[listBlock.Count - 1].transform.position;
            StartCoroutine(EffectControl.Move(listBlock[listBlock.Count - 1].gameObject, new Vector3(pos.x, 2.4f, pos.z), 0.6f, null));

            // Wrong block
            pos = listBlockBonus[listBlockBonus.Count - 1].transform.position;
            StartCoroutine(EffectControl.Move(listBlockBonus[listBlockBonus.Count - 1].gameObject, new Vector3(pos.x, 2.4f, pos.z), 0.6f, null));

            MainObjControl.Instant.arrowCtrl.MoveUpArrow();

        }

        CreatePath(p2, p3, listType[listType.Count - 2], isUp);
        CreateRoadBounus(p1, p2, p3, !listType[listType.Count - 2], isUp);

        RandTurn();
    }

    void RemovePoint()
    {
        listPointActive.RemoveAt(0);
        listType.RemoveAt(0);
        listBlock.RemoveAt(0);
        countDisable++;

        if (countDisable > 2)
        {
            countDisable--;
            Vector3 pos = listRoadActive[0].transform.position;
            StartCoroutine(Move(listRoadActive[0].gameObject, new Vector3(pos.x, -8, pos.z), 1));
            listRoadActive.RemoveAt(0);

            pos = listBonusActive[0].transform.position;
            StartCoroutine(Move(listBonusActive[0].gameObject, new Vector3(pos.x, -8, pos.z), 1));
            listBonusActive.RemoveAt(0);



            pos = listBlockBonus[0].transform.position;
            StartCoroutine(MoveBlock(listBlockBonus[0], new Vector3(pos.x, -8, pos.z), 1));
            listBlockBonus.RemoveAt(0);
        }
    }

    void CreatePath(Vector3 startPoint, Vector3 endPoint, bool isType1, bool isUp)
    {
        // Add path to listRoadActive
        // Add block to listBlock

        RoadUnit newRoad = GetRoad();
        listRoadActive.Add(newRoad);
        float x, y, z;
        Vector3 pos;
        GameDefine.Direction dir;

        if (startPoint.x == endPoint.x)
        {
            x = GameDefine.roadWidth;
            z = endPoint.z - startPoint.z - GameDefine.roadDistance;
            pos = startPoint + Vector3.forward * (0.5f * GameDefine.roadWidth + 0.5f * z + GameDefine.roadDistance);
            dir = GameDefine.Direction.Forward;
        }
        else
        {
            z = GameDefine.roadWidth;

            if (endPoint.x > startPoint.x)
            {
                x = endPoint.x - startPoint.x - GameDefine.roadDistance;
                pos = startPoint + Vector3.right * (0.5f * GameDefine.roadWidth + 0.5f * x + GameDefine.roadDistance);
                dir = GameDefine.Direction.Right;
            }
            else
            {
                x = -endPoint.x + startPoint.x - GameDefine.roadDistance;
                pos = startPoint + Vector3.left * (0.5f * GameDefine.roadWidth + 0.5f * x + GameDefine.roadDistance);
                dir = GameDefine.Direction.Left;
            }
        }

        y = GameDefine.roadTall;

        newRoad.transform.localScale = new Vector3(x, y, z);
        if (isUp)
        {
            newRoad.transform.position = new Vector3(pos.x, pos.y, pos.z);
        }
        else
        {
            newRoad.transform.position = new Vector3(pos.x, -8, pos.z);
        }

        BlockUnit unit = MainObjControl.Instant.blockCtrl.GetBlock(startPoint, dir, isType1, isUp);
        listBlock.Add(unit);
    }

    void CreateRoadBounus(Vector3 p1, Vector3 p2, Vector3 p3, bool isType1, bool isUp)
    {
        RoadUnit newRoad = GetRoad();
        newRoad.name = "roadX";
        listBonusActive.Add(newRoad);
        bool isLeft;
        float x, y, z;
        Vector3 pos;
        Vector3 blockPos;

        Vector3 Forward = new Vector3(0, 0, GameDefine.roadRandSize);
        Vector3 Left = new Vector3(-GameDefine.roadRandSize, 0, 0);
        Vector3 Right = new Vector3(GameDefine.roadRandSize, 0, 0);

        // Horizontal straight
        if (p1.z == p3.z)
        {
            x = GameDefine.roadWidth;
            z = GameDefine.roadRandSize;
            pos = p2 + Vector3.forward * (0.5f * GameDefine.roadWidth + 0.5f * z + GameDefine.roadDistance);
            blockPos = p2 + Vector3.forward * GameDefine.blockDistance;
            isLeft = false;
        }
        // Forward straight
        else if (p1.x == p3.x)
        {
            z = GameDefine.roadWidth;
            x = GameDefine.roadRandSize;

            pos = p2 + Vector3.left * (0.5f * GameDefine.roadWidth + 0.5f * x + GameDefine.roadDistance);
            blockPos = p2 + Vector3.left * GameDefine.blockDistance;
            isLeft = false;

            isLeft = true;
        }
        else
        {
            // Left/Right turn
            if (p1.x == p2.x)
            {
                x = GameDefine.roadWidth;
                z = GameDefine.roadRandSize;
                pos = p2 + Vector3.forward * (0.5f * GameDefine.roadWidth + 0.5f * z + GameDefine.roadDistance);
                blockPos = p2 + Vector3.forward * GameDefine.blockDistance;
                isLeft = false;
            }
            // Forward turn
            else
            {
                z = GameDefine.roadWidth;
                x = GameDefine.roadRandSize;

                // Left to Forward
                if (p1.x > p2.x)
                {
                    pos = p2 + Vector3.left * (0.5f * GameDefine.roadWidth + 0.5f * x + GameDefine.roadDistance);
                    blockPos = p2 + Vector3.left * GameDefine.blockDistance;
                }
                // Right to Forward
                else
                {
                    pos = p2 + Vector3.right * (0.5f * GameDefine.roadWidth + 0.5f * x + GameDefine.roadDistance);
                    blockPos = p2 + Vector3.right * GameDefine.blockDistance;
                }
                isLeft = true;
            }
        }

        y = GameDefine.roadTall;

        newRoad.transform.localScale = new Vector3(x, y, z);
        if (isUp)
        {
            newRoad.transform.position = new Vector3(pos.x, pos.y, pos.z);
        }
        else
        {
            newRoad.transform.position = new Vector3(pos.x, -8, pos.z);
        }
            
        BlockUnit unit = MainObjControl.Instant.blockCtrl.GetBlock(blockPos, isType1, isLeft, isUp);
        listBlockBonus.Add(unit);
    }

    RoadUnit GetRoad()
    {
        if (listRoadDisable.Count == 0)
        {
            return Instantiate(roadPrefab.gameObject).GetComponent<RoadUnit>();
        }
        else
        {
            RoadUnit newRoad = listRoadDisable[listRoadDisable.Count - 1];
            listRoadDisable.RemoveAt(listRoadDisable.Count - 1);
            return newRoad;
        }

    }

    IEnumerator Move(GameObject obj, Vector3 to, float duration)
    {
        float elapsed = 0;
        Vector3 from = obj.transform.position;

        while (elapsed <= duration)
        {
            obj.transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = to;

        listRoadDisable.Add(obj.GetComponent<RoadUnit>());
    }

    IEnumerator MoveBlock(BlockUnit unit, Vector3 to, float duration)
    {
        float elapsed = 0;
        Transform block = unit.transform;
        Vector3 from = block.position;

        while (elapsed <= duration)
        {
            block.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        block.position = to;

        MainObjControl.Instant.blockCtrl.FreeBlock(unit);
    }

    void RandTurn()
    {
        float rand = Random.value;
        switch (turn)
        {
            case GameDefine.Direction.Forward:
                if (rand > 0.66f)
                {
                    turn = GameDefine.Direction.Forward;
                }
                else if (rand > 0.33f)
                {
                    turn = GameDefine.Direction.Left;
                }
                else
                {
                    turn = GameDefine.Direction.Right;
                }
                break;
            case GameDefine.Direction.Left:
                if (rand > 0.5f)
                {
                    turn = GameDefine.Direction.Forward;
                }
                else
                {
                    turn = GameDefine.Direction.Left;
                }
                break;
            case GameDefine.Direction.Right:
                if (rand > 0.5f)
                {
                    turn = GameDefine.Direction.Forward;
                }
                else
                {
                    turn = GameDefine.Direction.Right;
                }
                break;
        }
    }

    public Vector3 GetEndPos()
    {
        return listBlockBonus[listBlockBonus.Count - 2].transform.position;
    }

    public Material GetEndMat()
    {
        return listBlock[0].GetComponent<MeshRenderer>().material;
    }

    readonly Vector3[] RoadDirections =
        {
            Vector3.left,
            Vector3.right,
            Vector3.forward
        };
    public RoadUnit ExtendPath(RoadUnit road, int depth)
    {
        if (depth < 0) return road;

        if (!road)
            road = CreateRoad(Vector3.zero, GameDefine.Direction.Forward, 14, true);

        if (depth == 0)
        {
            return road;
        }

        if (road.arrow == null)
        {
            Vector3 crossingPoint = road.startPoint + road.length * RoadDirections[(int)road.direction];
            ArrowUnit arrow = MainObjControl.Instant.arrowCtrl.CreateArrow(crossingPoint, road.direction);
            float[] nextLengths = { GameDefine.roadRandSize, GameDefine.roadRandSize };
            bool[] colors = { Random.value > 0.5 ? true : false, true };
            colors[1] = !colors[0];

            arrow.road = road;
            road.next = new RoadUnit[2];

            for (int i = 0; i < 2; i++)
            {
                GameDefine.Direction turn = arrow.directions[i];
                bool color = colors[i];
                float nextLength = nextLengths[i];

                road.next[i] = CreateRoad(crossingPoint, turn, nextLength, color);
                // MainObjControl.Instant.blockCtrl.GetBlock(crossingPoint, turn, color, true);
            }

            road.arrow = arrow;
        }
        
        if (depth >= 1)
            foreach (RoadUnit nextRoad in road.next)
                ExtendPath(nextRoad, depth - 1);

        return road;
    }

    public void RemovePath(RoadUnit road, int keepDepth)
    {
        if (road == null) return;

        if (road.next != null)
            foreach (RoadUnit nextRoad in road.next)
                RemovePath(nextRoad, keepDepth - 1);

        if (keepDepth <= 0)
        {
            road.gameObject.SetActive(false);
            if (road.arrow != null)
                road.arrow.gameObject.SetActive(false);
            if (road.block != null)
                road.block.gameObject.SetActive(false);
        }
    }

    // Return: Position of next crossing
    RoadUnit CreateRoad(Vector3 crossingPoint, GameDefine.Direction direction, float length, bool color)
    {
        // Add path to listRoadActive
        // Add block to listBlock

        bool onRoad = true;
        RoadUnit newRoad = GetRoad();
        BlockUnit unit = MainObjControl.Instant.blockCtrl.GetBlock(crossingPoint, direction, color, onRoad);

        newRoad.Set(crossingPoint, direction, length);
        newRoad.block = unit;
        unit.road = newRoad;

        activeRoads.Add(newRoad);
        activeBlocks.Add(unit);

        return newRoad;
    }
}
