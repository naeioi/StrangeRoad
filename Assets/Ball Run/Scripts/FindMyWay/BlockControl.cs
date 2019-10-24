using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockControl : MonoBehaviour
{
    public GameObject blockPrefab;

    public List<BlockUnit> idleBlock;
    public float tall, width, size;

    readonly Vector3[] BlockOffset =
    {
        Vector3.left * GameDefine.blockDistance,    // Left
        Vector3.right * GameDefine.blockDistance,   // Right
        Vector3.forward * GameDefine.blockDistance, // Forward
    };

    public BlockUnit GetBlock(Vector3 crossingPos, GameDefine.Direction dir, bool color, bool onRoad)
    {
        Vector3 blockPos = crossingPos;
        blockPos += BlockOffset[(int)dir];
        blockPos.y = onRoad ? 2.4f : -3.0f;

        BlockUnit unit = GetBlock();
        unit.transform.position = blockPos;
        unit.SetColor(color);
        unit.SetVisible(true);
        unit.crossingPos = crossingPos;
        unit.direction = dir;
        unit.color = color;

        if (dir != GameDefine.Direction.Forward)
        {
            unit.transform.localScale = new Vector3(width, tall, size);
        }
        else
        {
            unit.transform.localScale = new Vector3(size, tall, width);
        }

        return unit;
    }

    public BlockUnit GetBlock(Vector3 pos, bool isType1, bool isLeft, bool isUp)
    {
        if (isUp)
        {
            pos.y = 2.4f;
        }
        else
        {
            pos.y = -3;
        }

        BlockUnit unit = GetBlock();
        unit.transform.position = pos;
        unit.SetColor(isType1);
        unit.SetVisible(true);

        if (isLeft)
        {
            unit.transform.localScale = new Vector3(width, tall, size);
        }
        else
        {
            unit.transform.localScale = new Vector3(size, tall, width);
        }

        return unit;
    }

    public void FreeBlock(BlockUnit unit)
    {
        idleBlock.Add(unit);
    }

    BlockUnit GetBlock()
    {
        if (idleBlock.Count == 0)
        {
            return (Instantiate(blockPrefab) as GameObject).GetComponent<BlockUnit>(); 
        }
        else
        {
            BlockUnit newBlock = idleBlock[idleBlock.Count - 1];
            idleBlock.RemoveAt(idleBlock.Count - 1);
            return newBlock;
        }
    }

   

}
