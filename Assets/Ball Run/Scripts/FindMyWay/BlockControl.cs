using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockControl : MonoBehaviour
{
    public GameObject obsPrefab;
    public GameObject bonusPrefab;

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
        blockPos.y = onRoad ?  color ? 2.6f : 2.4f : -3.0f;

        BlockUnit unit = GetBlock(color);
        unit.transform.position = blockPos;
        unit.SetColor(color);
        unit.SetVisible(true);
        unit.crossingPos = crossingPos;
        unit.direction = dir;
        unit.color = color;

        if (!color)
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

    public void FreeBlock(BlockUnit unit)
    {
        idleBlock.Add(unit);
    }

    BlockUnit GetBlock(bool color)
    {
        if (color)
            return (Instantiate(bonusPrefab) as GameObject).GetComponent<BlockUnit>(); 
        else
            return (Instantiate(obsPrefab) as GameObject).GetComponent<BlockUnit>();
    }

   

}
