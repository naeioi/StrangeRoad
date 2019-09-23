using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OnClickCanvasButton : MonoBehaviour, IPointerClickHandler{
	public UnityEvent onClick;
    public Vector3 scaleZoom = new Vector3(1.2f, 1.2f, 1.2f);
	float timeZoomOut = 0.05f;
	float timeZoomIn = 0.1f;
    bool running = false;

    void OnEnable()
    {
        running = false;
        transform.localScale = Vector3.one;
    }

    public void OnPointerClick(PointerEventData eventData)
	{
        if (!running)
        {
            StartCoroutine(ButtonClick());
        }
	}
	
	public IEnumerator ButtonClick(){
        
        running = true;

		float currentTime = 0;
		
		while(currentTime < timeZoomOut)
		{
            transform.localScale = Vector3.Lerp(Vector3.one ,scaleZoom, currentTime/timeZoomOut);
			currentTime += Time.unscaledDeltaTime;
			yield return null;
		}
		
        onClick.Invoke();
        MainAudio.Main.PlaySound(TypeAudio.SoundClick);
		currentTime = 0;
		
		while(currentTime < timeZoomIn)
		{
            transform.localScale = Vector3.Lerp(scaleZoom, Vector3.one, currentTime/timeZoomIn);
			currentTime += Time.unscaledDeltaTime;
			yield return null;
		}
        
        //yield return new WaitForSeconds(0.3f);
        running = false;
	}
}

