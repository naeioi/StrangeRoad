using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockControl : MonoBehaviour
{
    public GameObject obsPrefab;
    public GameObject bonusPrefab;
    public GameObject diamondPrefab;

    public List<BlockUnit> idleBlock;
    public float tall, width, size;

    readonly Vector3[] BlockOffset =
    {
        Vector3.left * GameDefine.blockDistance,    // Left
        Vector3.right * GameDefine.blockDistance,   // Right
        Vector3.forward * GameDefine.blockDistance, // Forward
    };

    public BlockUnit GetEntryBlock(Vector3 crossingPos, GameDefine.Direction dir, bool correctRoad, bool onRoad)
    {
        Vector3 blockPos = crossingPos;
        blockPos += BlockOffset[(int)dir];
        blockPos.y = onRoad ?  correctRoad ? 2.6f : 2.4f : -3.0f;

        BlockUnit unit = GetEntryBlock(correctRoad);
        unit.transform.position = blockPos;
        unit.SetVisible(true);
        unit.crossingPos = crossingPos;
        unit.direction = dir;
        unit.color = correctRoad;

        if (!correctRoad)
        {
            // TODO: Refactor, put scaling logic in ObsBlock
            if (dir != GameDefine.Direction.Forward)
            {
                unit.transform.localScale = new Vector3(width, tall, size);
            }
            else
            {
                unit.transform.localScale = new Vector3(size, tall, width);
            }
        }

        return unit;
    }

    public BlockUnit GetExtraBlock(Vector3 crossingPos, GameDefine.Direction dir)
    {
        // Currently, diamond is the only extra block available
        Vector3 blockPos = crossingPos;
        blockPos += BlockOffset[(int)dir] * 2;
        blockPos.y = 2.6f;

        BlockUnit unit = Instantiate(diamondPrefab).GetComponent<BlockUnit>();
        unit.transform.position = blockPos;
        unit.SetVisible(true);
        unit.crossingPos = crossingPos;
        unit.direction = dir;

        return unit;
    }

    public void FreeBlock(BlockUnit unit)
    {
        idleBlock.Add(unit);
    }

    BlockUnit GetEntryBlock(bool correctRoad)
    {
        if (correctRoad)
            return (Instantiate(bonusPrefab) as GameObject).GetComponent<BlockUnit>(); 
        else
            return (Instantiate(obsPrefab) as GameObject).GetComponent<BlockUnit>();
    }

   

}
