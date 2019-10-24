using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LostScript : MonoBehaviour
{
    public Text bestText;
    public GameObject tap;
    public GameObject buttonPanel;

    Vector3 scaleIn = new Vector3(0, 1, 1);

    public void Reset()
    {
        SetActive(false);
        buttonPanel.transform.localScale = scaleIn;
        EffectControl.FadeOutCanvasNow(tap);
        EffectControl.FadeOutCanvasNow(bestText.gameObject);
    }

    void BlastBall()
    {
        PlayerControl player = MainObjControl.Instant.playerCtrl;
        player.Stop();

        MainAudio.Main.PlaySound(TypeAudio.SoundStop);

        player.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;

        Material blockMat = player.currentRoad.block.GetComponent<MeshRenderer>().material;
        MainObjControl.Instant.boomCtrl.ShowPlayerBoom(player.direction != GameDefine.Direction.Forward, blockMat);
    }

    public void GameOver()
    {
        BlastBall();

        MainState.SetState(MainState.State.Waiting);
        int time = Settings.GetTimer;
        if (time >= 4)
        {
            Settings.SetTimer(0);
        }
        else
        {
            Settings.SetTimer(time + 1);
        }


        SetActive(true);

        StartCoroutine(AnimGameOver());
       
    }

    IEnumerator AnimGameOver()
    {
        MainCanvas.Instance.inGameScript.MoveScoreDown();
        yield return new WaitForSeconds(0.5f);

        int score = MainCanvas.Instance.inGameScript.scoreInt;
        if (score > Settings.GetBest)
        {
            Settings.SetBest(score);
        }

        SetBestScore(Settings.GetBest);

        StartCoroutine(EffectControl.FadeInCanvas(bestText.gameObject, 0.5f));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(EffectControl.FadeInCanvas(tap, 0.5f));
        StartCoroutine(EffectControl.ScaleInCanvas(buttonPanel, 0.4f));
        yield return new WaitForSeconds(0.4f);
        MainState.SetState(MainState.State.GameOver);
        //        MainAudio.Main.PlaySound(TypeAudio.SoundLose);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && MainState.GetState == MainState.State.GameOver
            && !EventSystem.current.IsPointerOverGameObject(0))
        {
            MainAudio.Main.PlaySound(TypeAudio.SoundClick);
            MainCanvas.Instance.faderScript.Fade(new FaderControl.Callback0(MidleTryAgain));
        }
    }

    public void HomeButton()
    {
        MainCanvas.Instance.faderScript.Fade(new FaderControl.Callback0(MidleHome));
    }

    void MidleTryAgain()
    {
        Application.LoadLevel(Application.loadedLevel);
        StartCoroutine(WaitRestart());

    }


    IEnumerator WaitRestart()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        MainCanvas.Instance.Reset(true);
    }

    void MidleHome()
    {
        Application.LoadLevel(Application.loadedLevel);
        MainCanvas.Instance.Reset(false);
       
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void SetBestScore(int best)
    {
        bestText.text = "Best - " + best;
    }

    public void FbClick()
    {
        Application.OpenURL(""); // insert your link facebook here
    }

    public void RateClick()
    {
    }
}
