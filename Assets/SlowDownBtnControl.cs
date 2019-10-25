using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlowDownBtnControl : MonoBehaviour
{
    TextMeshProUGUI text;
    PlayerControl player
    {
        get
        {
            return MainObjControl.Instant.playerCtrl;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlowDown()
    {
        if (!player.isSlowingDown) {
            StartCoroutine(DoSlowDown());
        }
    }

    IEnumerator DoSlowDown()
    {
        player.SetSlowDown(true);
        text.fontSize = 72;
        for (int i = 5; i > 0; i--)
        {
            text.text = i.ToString() + "s";
            yield return new WaitForSeconds(1);
        }
        text.fontSize = 36;
        text.text = "Slow Down";
        player.SetSlowDown(false);
    }
}
