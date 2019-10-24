using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartGameScript : MonoBehaviour
{
    public RectTransform rec;
    public Sprite soundOnTexture, SoundOffTexture;
    public Image soundImage;
    bool soundOn = false;
    bool isClick = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && MainState.GetState == MainState.State.Home
            && !EventSystem.current.IsPointerOverGameObject(0))
        {
            StartPlayGame();
        }
    }

    public void Reset(bool isActive)
    {
        MainState.SetState(MainState.State.Home);

        if (!isActive)
        {
            Middle();
        }

        UpdateSound(Settings.GetSound == 1);
        SetActive(isActive);
    }

    public void UpdateSound(bool isSoundOn)
    {
        soundOn = !isSoundOn;
        ChangeSoundTexture(); 
    }

    void ChangeSoundTexture()
    {
        soundOn = !soundOn;
        if (soundOn)
        {
            soundImage.sprite = soundOnTexture;
            Settings.SetSound(1);
        }
        else
        {
            soundImage.sprite = SoundOffTexture;
            Settings.SetSound(0);
        }

        MainAudio.Main.MuteSound(soundOn);
    }

    public void Sound()
    {
        ChangeSound();
    }

    public void ChangeSound()
    {
        ChangeSoundTexture();
    }

    void StartPlayGame()
    {
        MainAudio.Main.PlaySound(TypeAudio.SoundClick);
        Middle();
       
    }

    void Middle()
    {
        MainState.SetState(MainState.State.Ingame);
        MainCanvas.Instance.inGameScript.SetActive(true);
        MainObjControl.Instant.playerCtrl.Run();
        // MainObjControl.Instant.camCtrl.allow = true;
        SetActive(false);

    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void FbClick()
    {
        Application.OpenURL(""); // insert your link facebook here
    }

    public void RateClick()
    {
    }

    public void MoreClick()
    {
        Application.OpenURL("");// insert your fb page playstore link
    }

}
