using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FaderControl : MonoBehaviour {
    public Image image;
    public float durationFade;
    public float durationFadeOut;
    public delegate void Callback0 ();

    public void Reset()
    {
        //SetActive(false);
    }

    public void Fade(Callback0 fn)
    {
        StartCoroutine(StartFade(fn));
    }

    void SetActive(bool isActive)
    {
        image.enabled = isActive;
    }

    IEnumerator StartFade(Callback0 fn)
    {
        SetActive(true);
        float timer = 0;
        Color color = image.color;

        image.color = new Color(color.r, color.g, color.b, 0);
        while(timer <= durationFade)
        {
            timer += Time.unscaledDeltaTime;
            image.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, timer / durationFade));
            yield return null;
        }

        image.color = new Color(color.r, color.g, color.b, 1);

        if (fn != null)
        {
            fn ();
        }
       

        timer = 0;
        while(timer <= durationFade)
        {
            timer += Time.unscaledDeltaTime;
            image.color = new Color(color.r, color.g, color.b, Mathf.Lerp(1, 0, timer / durationFade));
            yield return null;
        }

        image.color = new Color(color.r, color.g, color.b, 0);

        SetActive(false);
    }
}
