using UnityEngine;
using System.Collections;

public class ArrowUnit : MonoBehaviour
{

    public Transform arrow;
    public Transform road;
    public Transform roadR;
    public float duration;
    bool roting;

    void Start()
    {
        road.localScale = new Vector3(GameDefine.roadDistance, GameDefine.roadTall, GameDefine.roadWidth);
        road.localPosition = Vector3.left * 0.5f * (GameDefine.roadDistance + GameDefine.roadWidth);
    }

    public void StartRotate(Vector3 from, Vector3 to)
    {
        if (!roting)
        {
            StartCoroutine(RotateY(from, to));
        }
    }

    IEnumerator RotateY(Vector3 from, Vector3 to)
    {
        roting = true;
        float elapsed = 0;
        while (elapsed <= duration)
        {
            elapsed += Time.deltaTime;
            Vector3 t = Vector3.Lerp(from, to, elapsed / duration);
            roadR.localEulerAngles = t;
            arrow.localEulerAngles = t;
            yield return null;
        }
        roadR.localEulerAngles = to;
        arrow.localEulerAngles = to;
        roting = false;
    }

}
