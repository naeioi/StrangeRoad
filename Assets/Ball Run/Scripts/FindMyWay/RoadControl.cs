using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadControl : MonoBehaviour
{
    public GameObject roadPrefab;
    public int firstRoads;

    public List<GameObject> listRoadDisable;
    public List<GameObject> listRoadActive;
    public List<GameObject> listBonusActive;

    public List<Vector3> listPointActive;
    public List<Vector3> listPointDisable;
    public List<bool> listType;
    public List<BlockUnit> listBlock;
    public List<BlockUnit> listBlockBonus;
    int countDisable;


    public enum turn
    {
        left,
        right,
        forward,
    }

    public turn Turn;

    void Awake()
    {
        CreateFirstRoad();
        RandTurn();
    }

    public void PlayerCall()
    {
        Vector3 p1 = listPointActive[listPointActive.Count - 3];
        Vector3 p2 = listPointActive[listPointActive.Count - 2];
        Vector3 p3 = listPointActive[listPointActive.Count - 1];
        MainObjControl.Instant.arrowCrt.SetNewArrow(p1, p2, p3);
        RemovePoint();
        AddNewPoint(false);
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

    public void AddNewPoint(bool isUp)
    {
        Vector3 newPath;
        switch (Turn)
        {
            case turn.forward:
                newPath = new Vector3(0, 0, GameDefine.roadRandSize);
                break;
            case turn.left:
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
            Vector3 pos = listRoadActive[listRoadActive.Count - 1].transform.position;
            StartCoroutine(EffectControl.Move(listRoadActive[listRoadActive.Count - 1], new Vector3(pos.x, 0, pos.z), 0.4f, null));

            pos = listBonusActive[listBonusActive.Count - 1].transform.position;
            StartCoroutine(EffectControl.Move(listBonusActive[listBonusActive.Count - 1], new Vector3(pos.x, 0, pos.z), 0.4f, null));

            pos = listBlock[listBlock.Count - 1].transform.position;
            StartCoroutine(EffectControl.Move(listBlock[listBlock.Count - 1].gameObject, new Vector3(pos.x, 2.4f, pos.z), 0.6f, null));

            pos = listBlockBonus[listBlockBonus.Count - 1].transform.position;
            StartCoroutine(EffectControl.Move(listBlockBonus[listBlockBonus.Count - 1].gameObject, new Vector3(pos.x, 2.4f, pos.z), 0.6f, null));

            MainObjControl.Instant.arrowCrt.MoveUpArrow();

        }

        CreatePath(p2, p3, listType[listType.Count - 2], isUp);
        CreateRoadBounus(p1, p2, p3, !listType[listType.Count - 2], isUp);

        RandTurn();
    }

    public void RemovePoint()
    {
        listPointActive.RemoveAt(0);
        listType.RemoveAt(0);
        listBlock.RemoveAt(0);
        countDisable++;

        if (countDisable > 2)
        {
            countDisable--;
            Vector3 pos = listRoadActive[0].transform.position;
            StartCoroutine(Move(listRoadActive[0], new Vector3(pos.x, -8, pos.z), 1));
            listRoadActive.RemoveAt(0);

            pos = listBonusActive[0].transform.position;
            StartCoroutine(Move(listBonusActive[0], new Vector3(pos.x, -8, pos.z), 1));
            listBonusActive.RemoveAt(0);



            pos = listBlockBonus[0].transform.position;
            StartCoroutine(MoveBlock(listBlockBonus[0], new Vector3(pos.x, -8, pos.z), 1));
            listBlockBonus.RemoveAt(0);
        }
    }

    void CreatePath(Vector3 startPoint, Vector3 endPoint, bool isType1, bool isUp)
    {
        GameObject newRoad = GetRoad();
        listRoadActive.Add(newRoad);
        float x, y, z;
        Vector3 pos;
        bool isLeft;
        Vector3 blockPos;
        if (startPoint.x == endPoint.x)
        {
            x = GameDefine.roadWidth;
            z = endPoint.z - startPoint.z - GameDefine.roadDistance;
            pos = startPoint + Vector3.forward * (0.5f * GameDefine.roadWidth + 0.5f * z + GameDefine.roadDistance);
            blockPos = startPoint + Vector3.forward * GameDefine.blockDistance;
            isLeft = false;
        }
        else
        {
            z = GameDefine.roadWidth;

            if (endPoint.x > startPoint.x)
            {
                x = endPoint.x - startPoint.x - GameDefine.roadDistance;
                pos = startPoint + Vector3.right * (0.5f * GameDefine.roadWidth + 0.5f * x + GameDefine.roadDistance);
                blockPos = startPoint + Vector3.right * GameDefine.blockDistance;
            }
            else
            {
                x = -endPoint.x + startPoint.x - GameDefine.roadDistance;
                pos = startPoint + Vector3.left * (0.5f * GameDefine.roadWidth + 0.5f * x + GameDefine.roadDistance);
                blockPos = startPoint + Vector3.left * GameDefine.blockDistance;
            }
            isLeft = true;
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

        BlockUnit unit = MainObjControl.Instant.colorBlockCrt.GetBlock(blockPos, isType1, isLeft, isUp);
        listBlock.Add(unit);
    }

    void CreateRoadBounus(Vector3 p1, Vector3 p2, Vector3 p3, bool isType1, bool isUp)
    {
        GameObject newRoad = GetRoad();
        newRoad.name = "roadX";
        listBonusActive.Add(newRoad);
        bool isLeft;
        float x, y, z;
        Vector3 pos;
        Vector3 blockPos;

        if (p1.z == p3.z)
        {
            x = GameDefine.roadWidth;
            z = GameDefine.roadRandSize;
            pos = p2 + Vector3.forward * (0.5f * GameDefine.roadWidth + 0.5f * z + GameDefine.roadDistance);
            blockPos = p2 + Vector3.forward * GameDefine.blockDistance;
            isLeft = false;
        }
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
            if (p1.x == p2.x)
            {
                x = GameDefine.roadWidth;
                z = GameDefine.roadRandSize;
                pos = p2 + Vector3.forward * (0.5f * GameDefine.roadWidth + 0.5f * z + GameDefine.roadDistance);
                blockPos = p2 + Vector3.forward * GameDefine.blockDistance;
                isLeft = false;
            }
            else
            {
                z = GameDefine.roadWidth;
                x = GameDefine.roadRandSize;

                if (p1.x > p2.x)
                {
                    pos = p2 + Vector3.left * (0.5f * GameDefine.roadWidth + 0.5f * x + GameDefine.roadDistance);
                    blockPos = p2 + Vector3.left * GameDefine.blockDistance;
                }
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
            
        BlockUnit unit = MainObjControl.Instant.colorBlockCrt.GetBlock(blockPos, isType1, isLeft, isUp);
        listBlockBonus.Add(unit);
    }

    GameObject GetRoad()
    {
        if (listRoadDisable.Count == 0)
        {
            return Instantiate(roadPrefab) as GameObject;
        }
        else
        {
            GameObject newRoad = listRoadDisable[listRoadDisable.Count - 1];
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

        listRoadDisable.Add(obj);
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

        MainObjControl.Instant.colorBlockCrt.SetBlock(unit);
    }

    void RandTurn()
    {
        float rand = Random.value;
        switch (Turn)
        {
            case turn.forward:
                if (rand > 0.66f)
                {
                    Turn = turn.forward;
                }
                else if (rand > 0.33f)
                {
                    Turn = turn.left;
                }
                else
                {
                    Turn = turn.right;
                }
                break;
            case turn.left:
                if (rand > 0.5f)
                {
                    Turn = turn.forward;
                }
                else
                {
                    Turn = turn.left;
                }
                break;
            case turn.right:
                if (rand > 0.5f)
                {
                    Turn = turn.forward;
                }
                else
                {
                    Turn = turn.right;
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
}
