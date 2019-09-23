using UnityEngine;
using System.Collections;

public class UIShaker : MonoBehaviour {
	public float preDelay;
	public float duration;
	public float delay;
	public float fromAngle;
	public float toAngle;	

	float preDelayOrigin;
	float durationOrigin;
	float delayOrigin;
	float fromAngleOrigin;
	float toAngleOrigin;	

	bool running = true;

	void OnEnable() {
		gameObject.transform.rotation = Quaternion.identity;
		StartCoroutine(StartRotate());
	}

	void Start() {
		if (duration != 0) {
			this.preDelayOrigin = preDelay;
			this.durationOrigin = duration;
			this.delayOrigin = delay;
			this.fromAngleOrigin = fromAngle;
			this.toAngleOrigin = toAngle;
		}
	}

	public void Reset() {
		if (delayOrigin != 0) {
			running = true;
			this.preDelay = preDelayOrigin;
			this.duration = durationOrigin;
			this.delay = delayOrigin;
			this.fromAngle = fromAngleOrigin;
			this.toAngle = toAngleOrigin;
		}
	}

	public void Pause() {
		running = false;
	}

	public void Resume() {
		running = true;
	}

	public void ShakeVibrate(float duration = 0.02f, float fromAngle = 5, float toAngle = -5) {
		delay = 0;
		this.duration = duration;
		this.fromAngle = fromAngle;
		this.toAngle = toAngle;
	}

	IEnumerator StartRotate() {
		if (preDelay > 0) {
			yield return new WaitForSeconds(preDelay);
		}
		                  
		while (true) {
			if (running) {
			float timeCounter = 0;
			float rotateTime = duration * 0.25f;
			while (timeCounter < rotateTime) {
				timeCounter += Time.deltaTime;
				float zAngle = Mathf.Lerp(0, fromAngle, timeCounter / rotateTime);
				var currentAngle = gameObject.transform.rotation;
				gameObject.transform.rotation = Quaternion.Euler(currentAngle.x, currentAngle.y, zAngle);
				yield return null;
			}

			timeCounter = 0;
			rotateTime = duration * 0.5f;
			while (timeCounter < rotateTime) {
				timeCounter += Time.deltaTime;
				float zAngle = Mathf.Lerp(fromAngle, toAngle, timeCounter / rotateTime);
				var currentAngle = gameObject.transform.rotation;
				gameObject.transform.rotation = Quaternion.Euler(currentAngle.x, currentAngle.y, zAngle);
				yield return null;
			}

			timeCounter = 0;
			rotateTime = duration * 0.25f;
			while (timeCounter < rotateTime) {
				timeCounter += Time.deltaTime;
				float zAngle = Mathf.Lerp(toAngle, 0, timeCounter / rotateTime);
				var currentAngle = gameObject.transform.rotation;
				gameObject.transform.rotation = Quaternion.Euler(currentAngle.x, currentAngle.y, zAngle);
				yield return null;
			}
			}
			yield return new WaitForSeconds(delay);
		}
	}	
}
