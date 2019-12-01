using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDiamondCtrl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MainCanvas.Instance.inGameScript.diamondCount += 1;
    }
}
