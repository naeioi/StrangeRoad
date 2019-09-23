using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISpread : MonoBehaviour {
	public float Duration = 0.6f;

	float t = 0;
	public float delayStart = 0;
	public float fromScale;
	public float toScale;
	public float fromOpacity;
	public float toOpacity;
	public bool repeating;
    Graphic image;
	bool enableAnimating = true;

	Vector3 frSc;
	Vector3 toSc;
	bool starting;

	public bool DoneAnimate {get{return t >= Duration;}}

	public void StopAction() {
		enableAnimating = false;
	}

	public void Reset() {
		enableAnimating = true;
		Start();
	}

	void Start () {		
		if (enableAnimating) {
			t = 0;
			starting = false;
            image = GetComponent<Graphic>();
			SetOpacity(fromOpacity);
			frSc = Vector3.one * fromScale;
			toSc = Vector3.one * toScale;
			transform.localScale = frSc;
		}
	}
	
	void Update () {
		if (!enableAnimating) {
			return;
		}
		if (!starting) {
            t += Time.deltaTime;
			if (t >= delayStart) {
				t = 0;
				starting = true;
			}
		}
		else if (starting && t < Duration) {
            t += Time.deltaTime;
			transform.localScale = Vector3.Lerp(frSc, toSc, t / Duration);
			SetOpacity(Mathf.Lerp(fromOpacity, toOpacity, t / Duration));

			if (t >= Duration && repeating) {
				t = 0;
				var tmpScale = frSc;
				frSc = toSc;
				toSc = tmpScale;
				float tmpOpacity = fromOpacity;
				fromOpacity = toOpacity;
				toOpacity = tmpOpacity;
			}
		}
	}

	void SetOpacity(float opacity) {
		var color = image.color;
		color.a = opacity;
		image.color = color;
	}

	public bool EnableAnimating {
		get {
			return enableAnimating;
		}
		set {
			enableAnimating = value;
		}
	}
}
