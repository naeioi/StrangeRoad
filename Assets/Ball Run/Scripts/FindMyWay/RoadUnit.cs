using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoadUnit : MonoBehaviour
{
    public Vector3 startPoint;
    public GameDefine.Direction direction;
    public float length;

    public ArrowUnit arrow;
    public BlockUnit block;
    public RoadUnit[] next;

    public void Set(Vector3 startPoint, GameDefine.Direction direction, float length)
    {
        this.startPoint = startPoint;
        this.length = length;
        this.direction = direction;
        arrow = null;
        block = null;
        next = null;

        Vector3 scale;
        Vector3 pos;

        float scaleLength = length - GameDefine.roadDistance;

        if (direction == GameDefine.Direction.Forward)
        {
            scale.x = GameDefine.roadWidth;
            scale.z = scaleLength;
            pos = startPoint + Vector3.forward * (0.5f * GameDefine.roadWidth + 0.5f * scale.z + GameDefine.roadDistance);
        }
        else
        {
            scale.z = GameDefine.roadWidth;

            if (direction == GameDefine.Direction.Right)
            {
                scale.x = scaleLength;
                pos = startPoint + Vector3.right * (0.5f * GameDefine.roadWidth + 0.5f * scale.x + GameDefine.roadDistance);
            }
            else
            {
                scale.x = scaleLength;
                pos = startPoint + Vector3.left * (0.5f * GameDefine.roadWidth + 0.5f * scale.x + GameDefine.roadDistance);
            }
        }

        scale.y = GameDefine.roadTall;

        transform.localScale = scale;
        transform.position = pos;
    }

    public RoadUnit GetNextByDirection(GameDefine.Direction dir)
    {
        if (next == null) return null;

        foreach (RoadUnit road in next)
            if (road.direction == dir)
            {
                return road;
            }

        return null;
    }
}
