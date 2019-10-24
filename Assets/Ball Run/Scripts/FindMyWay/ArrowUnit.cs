using UnityEngine;
using System.Collections;

public class ArrowUnit : MonoBehaviour
{
    public float f;

    public GameDefine.Direction[] directions;
    public RoadUnit road;

    public Transform arrow;
    public Transform bridge;
    public Transform bridgeR;
    public float duration;
    bool rotating;
    int turn;

    readonly Vector3[] RotationsByDirection =
    {
        Vector3.zero,           // Left
        new Vector3(0, 180, 0), // Right
        new Vector3(0, 90, 0),  // Forward
    };

    Vector3 targetRotation
    {
        get
        {
            return RotationsByDirection[(int)directions[turn]];
        }
    }

    Vector3 oppositeRotation
    {
        get
        {
            return RotationsByDirection[(int)directions[1 - turn]];
        }
    }

    void Start()
    {
        bridge.localScale = new Vector3(GameDefine.roadDistance, GameDefine.roadTall, GameDefine.roadWidth);
        bridge.localPosition = Vector3.left * 0.5f * (GameDefine.roadDistance + GameDefine.roadWidth);
    }

    public GameDefine.Direction GetDirection()
    {
        return directions[turn];
    }

    public GameDefine.Direction GetOppositeDirection()
    {
        return directions[1 - turn];
    }

    public void SetPosition(Vector3 pos, GameDefine.Direction pathDir, GameDefine.Direction[] turns)
    {
        this.directions = turns;

        // Make arrow off track
        Vector3 arrowOffset;
        if (pathDir == GameDefine.Direction.Forward)
        {
            if (System.Array.IndexOf(turns, GameDefine.Direction.Left) != -1)
                arrowOffset = Vector3.right * f;
            else
                arrowOffset = Vector3.left * f;
        }
        else
        {
            arrowOffset = Vector3.back * f;
        }

        arrowOffset.y = arrow.localPosition.y;
        arrow.localPosition = arrowOffset;
        transform.position = pos;

        // Assign an initial rotation
        turn = Random.Range(0, 2);
        Vector3 initRot = RotationsByDirection[(int)turns[turn]];
        arrow.transform.eulerAngles = initRot;
        bridgeR.transform.eulerAngles = initRot;
    }

    public void StartToggle()
    {
        if (!rotating)
        {
            turn = 1 - turn;
            StartCoroutine(RotateY(oppositeRotation, targetRotation));
        }
    }

    public void StartRotate(Vector3 from, Vector3 to)
    {
        if (!rotating)
        {
            StartCoroutine(RotateY(from, to));
        }
    }

    IEnumerator RotateY(Vector3 from, Vector3 to)
    {
        rotating = true;
        float elapsed = 0;
        while (elapsed <= duration)
        {
            elapsed += Time.deltaTime;
            Vector3 t = Vector3.Lerp(from, to, elapsed / duration);
            bridgeR.localEulerAngles = t;
            arrow.localEulerAngles = t;
            yield return null;
        }
        bridgeR.localEulerAngles = to;
        arrow.localEulerAngles = to;
        rotating = false;
    }

}
