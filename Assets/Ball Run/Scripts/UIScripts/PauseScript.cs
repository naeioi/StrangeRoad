using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    

    public void Reset()
    {
        SetActive(false);
    }

    public void PauseGame()
    {
        MainCanvas.Instance.faderScript.Fade(new FaderControl.Callback0(Middle));
    }

    void Middle()
    {
        SetActive(true);
        MainState.SetState(MainState.State.Pause);
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        MainCanvas.Instance.faderScript.Fade(new FaderControl.Callback0(MidleUnPause));
    }

    void MidleUnPause()
    {
        Time.timeScale = 1;
        MainState.SetState(MainState.State.Ingame);
        SetActive(false);
    }

    public void GoHome()
    {
        MainCanvas.Instance.faderScript.Fade(new FaderControl.Callback0(MidleHome));
    }

    void MidleHome()
    {
        Time.timeScale = 1;
//        MainObjControl.Instant.grid.SetAllCube();
//        MainObjControl.Instant.control.SetAllCube();
//        MainCanvas.Main.Reset(false);
//        MainState.SetState(MainState.State.Home);
    }

    public void Restart()
    {
        MainCanvas.Instance.faderScript.Fade(new FaderControl.Callback0(MidleRestart));
    }

    void MidleRestart()
    {
        Time.timeScale = 1;
//        MainObjControl.Instant.grid.SetAllCube();
//        MainObjControl.Instant.control.SetAllCube();
//        MainCanvas.Main.Reset(true);

    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
