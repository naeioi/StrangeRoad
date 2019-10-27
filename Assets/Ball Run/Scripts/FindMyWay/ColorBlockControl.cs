using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorBlockControl : MonoBehaviour
{
    public GameObject blockPrefab;
    public List<BlockUnit> listBlock;
    public float tall, width, size;

    public BlockUnit GetBlock(Vector3 pos, bool isType1, bool isLeft, bool isUp)
    {
        pos.x = pos.x;
        if (isUp)
        {
            pos.y = 2.4f;
        }
        else
        {
            pos.y = -3;
        }

        pos.z = pos.z;
        BlockUnit unit = GetBlock();
        unit.transform.position = pos;
        unit.SetColor(isType1);

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

    public void SetBlock(BlockUnit unit)
    {
        listBlock.Add(unit);
    }


    BlockUnit GetBlock()
    {
        if (listBlock.Count == 0)
        {
            return (Instantiate(blockPrefab) as GameObject).GetComponent<BlockUnit>(); 
        }
        else
        {
            BlockUnit newBlock = listBlock[listBlock.Count - 1];
            listBlock.RemoveAt(listBlock.Count - 1);
            return newBlock;
        }
    }

   

}
