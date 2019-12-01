using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InGameScript : MonoBehaviour
{
    public RectTransform rec;
    public RectTransform recScore;
    public Text scoreTxt;
    public Text levelTxt;
    private int _scoreInt;
    public float scoreDuration;
    public Vector2 to, from;
    public Vector3 fromScale;

    public DiamonBtnControl diamondCtrl;
    public int _diamondCount;
    public bool isDoublingScore;

    public delegate void ScoreChangeListener(int score);
    public HashSet<ScoreChangeListener> scoreChangeListeners;

    public int diamondCount
    {
        get => _diamondCount;
        set
        {
            _diamondCount = value;
            diamondCtrl.DiamondCountUpdated();
        }
    }

    public int scoreInt
    {
        get => _scoreInt;
        set
        {
            _scoreInt = value;
            foreach (var listener in scoreChangeListeners)
                listener(value);
        }
    }

    private void Awake()
    {
        isDoublingScore = false;
        diamondCount = 0;
        scoreChangeListeners = new HashSet<ScoreChangeListener>();
    }

    public void Reset(bool isActive)
    {
        scoreInt = 0;
        isDoublingScore = false;
        diamondCount = 0;
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
        int incrScore = isDoublingScore ? 2 * value : value;
        scoreInt += incrScore;
        SetScoreTxt(scoreInt * 100);
    }
    
    //decrease score after tapping item button
    public bool DecrScore(int value = 10)
    {
        Debug.Log("scoreint:" + scoreInt.ToString() + " value:"+value.ToString());
        if(scoreInt < value)
            return false;
        scoreInt -= value;
        SetScoreTxt(scoreInt * 100);
        return true;
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
