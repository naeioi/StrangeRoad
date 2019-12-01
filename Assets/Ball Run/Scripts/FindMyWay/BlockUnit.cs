using UnityEngine;
using System.Collections;

public class BlockUnit : MonoBehaviour
{
    public RoadUnit road;
    public Vector3 crossingPos;
    public GameDefine.Direction direction;
    public bool color;

    void Awake()
    {
    }

    public void SetVisible(bool visible)
    {
        GetComponent<MeshRenderer>().enabled = visible;
    }
}
