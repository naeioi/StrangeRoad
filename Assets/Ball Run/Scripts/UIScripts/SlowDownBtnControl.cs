using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlowDownBtnControl : ItemBtnControl
{
    public void SlowDown()
    {
        if(!MainCanvas.Instance.inGameScript.DecrScore(price / 100))
            return;
        // player.slowdownTime = 5.0;
        if (!player.slowingDown)
        {
            player.SetSlowDown(true);
            Play(() => player.SetSlowDown(false));
        }
    }
}
