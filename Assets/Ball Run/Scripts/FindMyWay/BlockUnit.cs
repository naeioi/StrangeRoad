using UnityEngine;
using System.Collections;

public class BlockUnit : MonoBehaviour
{
    public Color top1;
    public Color left1;
    public Color front1;

    public Color top2;
    public Color left2;
    public Color front2;

    Material mat;

    public RoadUnit road;
    public Vector3 crossingPos;
    public GameDefine.Direction direction;
    public bool color;

    void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    public void SetVisible(bool visible)
    {
        GetComponent<MeshRenderer>().enabled = visible;
    }

    public void SetColor(bool isType1)
    {
        if (isType1)
        {
            mat.SetColor("_TopColor", top1);
            mat.SetColor("_FrontColor", front1);
            mat.SetColor("_LeftColor", left1);
        }
        else
        {
            mat.SetColor("_TopColor", top2);
            mat.SetColor("_FrontColor", front2);
            mat.SetColor("_LeftColor", left2);
        }

    }
}
