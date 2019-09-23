using UnityEngine;
using System.Collections;

public class ListenBackButton : MonoBehaviour {

	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (MainState.GetState)
            {
                case MainState.State.Home:
                    //Application.Quit();
                    break;
                case MainState.State.Ingame:
                    MainCanvas.Main.pauseScript.PauseGame();
                    break;
                case MainState.State.GameOver:
                    BackHome();
                    break;
            }
        }
	
	}

    void BackHome()
    {
        MainCanvas.Main.faderScript.Fade(new FaderControl.Callback0(MidleHome));
    }

    void MidleHome()
    {
        MainCanvas.Main.Reset(false);
    }
}
