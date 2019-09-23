using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackGroundScript : MonoBehaviour {
    
    public void Reset(bool isActive)
    {
        SetActive(isActive);
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

}
