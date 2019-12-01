using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadControl : MonoBehaviour
{
    public RoadUnit roadPrefab;

    public List<RoadUnit> activeRoads;
    public List<BlockUnit> activeBlocks;

    public GameDefine.Direction turn;
    public PlayerControl player;

    void Awake()
    {
        player = MainObjControl.Instant.playerCtrl;

        RoadUnit rootRoad = ExtendPath(null, 1);
        player.currentRoad = rootRoad;
    }

    RoadUnit GetRoad()
    {
        return Instantiate(roadPrefab.gameObject).GetComponent<RoadUnit>();
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
                road.next[i].father = road;
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

    public void RemovePreviousPaths(RoadUnit road)
    {
        if (road.GetFather() == null)
            return;
        RoadUnit grandfatherRoad = road.GetFather().GetFather();
        if (grandfatherRoad == null)
        {
            RoadUnit freeRoad = road.GetFather();
            freeRoad.gameObject.SetActive(false);
            if (freeRoad.arrow != null)
                freeRoad.arrow.gameObject.SetActive(false);
            if (freeRoad.block != null)
                freeRoad.block.gameObject.SetActive(false);   
            return; 
        }
        foreach (RoadUnit freeRoad in grandfatherRoad.next)
        {
            freeRoad.gameObject.SetActive(false);
            if (freeRoad.arrow != null)
                freeRoad.arrow.gameObject.SetActive(false);
            if (freeRoad.block != null && freeRoad.direction != road.GetFather().direction)
                freeRoad.block.gameObject.SetActive(false);
        }
        return;
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
