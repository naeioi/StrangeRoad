using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SmashBtnControl : MonoBehaviour
{
    TextMeshProUGUI text;

    PlayerControl player
    {
        get
        {
            return MainObjControl.Instant.playerCtrl;
        }
    }

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Smash()
    {
        if (!player.smashing)
            StartCoroutine(DoSmash());
    }

    IEnumerator DoSmash()
    {
        player.SetSmash(true);
        text.fontSize = 72;
        for (int i = 5; i > 0; i--)
        {
            text.text = i.ToString() + "s";
            yield return new WaitForSeconds(1);
        }
        text.fontSize = 36;
        text.text = "Smash";
        player.SetSmash(false);
    }
}
