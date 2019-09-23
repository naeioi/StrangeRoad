using UnityEngine;
using System.Collections;

public class ScaleInOut : MonoBehaviour {

    public Vector3 to;
    public float duration;

    public void StartAnim()
    {
        StartCoroutine(StartScaleInOut());
    }

    IEnumerator StartScaleInOut()
    {
        while (true)
        {
            float timer = 0;

            while (timer <= duration)
            {
                timer += Time.deltaTime;
                transform.localScale = Vector3.Lerp(Vector3.one, to, timer /duration );
                yield return null;
            }

            yield return null;
            timer = 0;

            while (timer <= duration)
            {
                timer += Time.deltaTime;
                transform.localScale = Vector3.Lerp(to, Vector3.one, timer /duration );
                yield return null;
            }
        }
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public bool iSActive
    {
        get{ return gameObject.activeSelf;}
    }
}
