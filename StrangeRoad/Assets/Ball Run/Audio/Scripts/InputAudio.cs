using UnityEngine;
using System.Collections;

[System.Serializable]
public struct InputAudio {
	
	[SerializeField]
	public TypeAudio type;
	
	[SerializeField]
	public AudioClip audioClip;
}