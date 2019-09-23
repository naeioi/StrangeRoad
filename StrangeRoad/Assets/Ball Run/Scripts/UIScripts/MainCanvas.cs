using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    static MainCanvas mainCanvas;
    public BackGroundScript backGroundScript;
    public InGameScript inGameScript;
    public StartGameScript startGameScript;
    public LostScript lostScript;
    public PauseScript pauseScript;
    public FaderControl faderScript;

    void Awake()
    {
        mainCanvas = this;
    }

    void Start()
    {
        Reset(false);
    }

    public static MainCanvas Main
    {
        get { return mainCanvas; }
    }

    public void Reset(bool isContinue)
    {
        Time.timeScale = 1;
        MainState.SetState(MainState.State.Home);

//        // Canvas

        startGameScript.Reset(!isContinue);
        inGameScript.Reset(isContinue);
        lostScript.Reset();
        pauseScript.Reset();
        faderScript.Reset();
//      other


    }
}
