using UnityEngine;
using System.Collections;

public class GameDefine
{

    public static Vector2 posRight = new Vector2(2000, 0);
    public static Vector2 posLeft = new Vector2(-2000, 0);
    public static Vector2 posTop = new Vector2(0, 2000);
    public static Vector2 posCenter = new Vector2(0, 0);

    public static string pageID = "";
// insert your fb page ID
    public static string pageName = "";
// insert your fb page name

    public static float roadTall = 4;
    public static float roadWidth = 1.5f;

    public static float roadRandSize
    {
        get{ return Random.Range(9.5f, 18.0f); }
    }

    public enum Direction
    {
        Left, Right, Forward
    }

    public static float roadDistance = 3.0f;
    public static float blockDistance = 4.0f;

}
