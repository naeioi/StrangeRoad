using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameScript : MonoBehaviour
{
    public RectTransform rec;
    public RectTransform recScore;
    public Text scoreTxt;
    public Text levelTxt;
    public int scoreInt;
    public float scoreDuration;
    public Vector2 to, from;
    public Vector3 fromScale;

    public void Reset(bool isActive)
    {
        scoreInt = 0;
        SetScoreTxt(0);
        // recScore.anchoredPosition = from;
        // recScore.localScale = fromScale;
        SetActive(isActive);
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

   
        
    public void Pause()
    {
        MainCanvas.Instance.pauseScript.PauseGame();
    }

    public void IncrScore(int value = 1)
    {
        scoreInt += value;
        SetScoreTxt(scoreInt * 100);
    }
    
    //decrease score after tapping item button
    public void DecrScore(int value = 1)
    {
        scoreInt -= value;
        SetScoreTxt(scoreInt * 100);
    }

    public void SetScoreTxt(int value)
    {
        scoreTxt.text = value.ToString();
    }

    public void SetLevelTxt(int value)
    {
        levelTxt.text = "Level " + value.ToString();
    }

    public void MoveScoreDown()
    {
        // StartCoroutine(AnimScore());
    }

    IEnumerator AnimScore()
    {
        float elapsed = 0;
        while (elapsed <= scoreDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / scoreDuration;
            recScore.anchoredPosition = Vector2.Lerp(from, to, t);
            recScore.localScale = Vector3.Lerp(fromScale, Vector3.one, t);
            yield return null;
        }

        recScore.anchoredPosition = to;
        recScore.localScale = Vector3.one;
    }
}
