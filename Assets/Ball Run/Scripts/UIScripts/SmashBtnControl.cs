using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SmashBtnControl : ItemBtnControl
{
    public void Smash()
    {
        if(!MainCanvas.Instance.inGameScript.DecrScore(price / 100))
            return;
        if (!player.smashing)
        {
            player.SetSmash(true);
            play(() => player.SetSmash(false));
        }
    }
}
