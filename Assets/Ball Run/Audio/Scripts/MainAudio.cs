using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;

public class MainAudio : MonoBehaviour {
	static MainAudio main;
	public InputAudio[] listInputAudio;

	AudioSource[] sound;
	Dictionary< TypeAudio, AudioSource> audioDict = new Dictionary<TypeAudio, AudioSource>();
	public bool isMute;
    int bgIndex;

	void Awake()
	{
		AddComponienAudioSources();
		main = this;
	}

    void Start()
    {
        audioDict[TypeAudio.SoundBG].loop = true;
        audioDict[TypeAudio.SoundBG].volume = 0.6f;
        PlaySound(TypeAudio.SoundBG);
    }

	public static MainAudio Main
	{
		get{ return main;}
	}

	void AddComponienAudioSources()
	{
		sound = new AudioSource[listInputAudio.Length];
		for(int i = 0; i < listInputAudio.Length; i++)
		{
			AudioSource thisAudio = gameObject.AddComponent<AudioSource>();
			sound[i] = thisAudio;
			thisAudio.playOnAwake = false;
			thisAudio.clip = listInputAudio[i].audioClip;
			audioDict.Add(listInputAudio[i].type, thisAudio);
		}
	}

    public void StopBGSound()
    {
        audioDict[TypeAudio.SoundBG].Stop();
    }

    public void PlaySound(TypeAudio type)
	{
		if(!isMute)
		{
			audioDict[type].Play();
		}
	}

	public void MuteSound(bool bol)
	{
		isMute  = !bol;

        if (isMute)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }

	}
}
