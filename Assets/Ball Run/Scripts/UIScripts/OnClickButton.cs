using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public abstract class OnClickButton : MonoBehaviour {
	public int number;
	public float scaleZoom = 1.2f;
	public float distanceScale;
	float timeZoomOut = 0.1f;
	float timeZoomIn = 0.2f;
	Vector3 fromScale;
	Vector3 toScale;
	public bool onPos = false;
	public bool running = false;

	void Start()
	{
		fromScale = transform.localScale;
		toScale = fromScale * scaleZoom;
		distanceScale = toScale.x - fromScale.x;
	}

	public abstract void ClickedButton();

	void Update()
	{
		if(Input.GetMouseButtonUp(0))
		{
			if(onPos && !running)
			{
				onPos = false;
				ClickedButton();
				StopAllCoroutines();
				StartCoroutine("ButtonClickEnd");
			}
		}
	}

	void OnMouseEnter()
	{
		onPos = true;
		if(!running)
		{
			StopAllCoroutines();
			StartCoroutine("ButtonClickZoomOut");
		}
	}

	void OnMouseDown()
	{
		onPos = true;
		
	}

	void OnMouseExit()
	{
		onPos = false;
		if(!running)
		{
			StopAllCoroutines();
			StartCoroutine("ButtonClickZoomIn");
		}

	}

	public IEnumerator ButtonClickZoomOut(){
		float currentTime = timeZoomOut * (transform.localScale.x - fromScale.x) / distanceScale;
		
		while(currentTime < timeZoomOut)
		{
			transform.localScale = Vector3.Lerp(fromScale,toScale,currentTime/timeZoomOut);
			currentTime += Time.deltaTime;
			yield return null;
		}
		
	}
	
	public IEnumerator ButtonClickZoomIn(){
		float currentTime = timeZoomIn * (-transform.localScale.x + toScale.x) / distanceScale;
		while(currentTime < timeZoomIn)
		{
			transform.localScale = Vector3.Lerp(toScale,fromScale,currentTime/timeZoomIn);
			currentTime += Time.deltaTime;
			yield return null;
		}
	}

	public IEnumerator ButtonClickEnd(){
		running = true;
		float currentTime = timeZoomOut * (transform.localScale.x - fromScale.x) / distanceScale;
		
		while(currentTime < timeZoomOut)
		{
			transform.localScale = Vector3.Lerp(fromScale,toScale,currentTime/timeZoomOut);
			currentTime += Time.deltaTime;
			yield return null;
		}

		currentTime = 0;

		while(currentTime < timeZoomIn)
		{
			transform.localScale = Vector3.Lerp(toScale,fromScale,currentTime/timeZoomIn);
			currentTime += Time.deltaTime;
			yield return null;
		}

		running = false;
	}
}
