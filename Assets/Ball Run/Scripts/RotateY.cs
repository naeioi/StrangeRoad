using UnityEngine;
using System.Collections;

public class RotateY : MonoBehaviour {
	public float speed;
	public bool randomDirect;
	int rotateRight = 1;
	
	void OnEnable ()
	{
		if (randomDirect) {
			if (Random.value > 0.5f) {
				rotateRight = -1;
			} else {
				rotateRight = 1;
			}
		}
	}
	
	void Update ()
	{
		transform.Rotate (new Vector3 (0, -speed * rotateRight * Time.deltaTime, 0), Space.Self);
		
	}
}
