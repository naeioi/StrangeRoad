using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SmashBtnControl : MonoBehaviour
{
    PlayerControl player;
    TextMeshProUGUI text;

    private void Start()
    {
        player = MainObjControl.Instant.playerCtrl;
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Smash()
    {
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
