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
        GetComponent<Image>().color = new Color(0.3f, 0.4f, 0.6f); // 初始按钮置灰
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlowDown()
    {
        if(!MainCanvas.Instance.inGameScript.DecrScore())
            return;
        player.slowdownTime = 5.0;
        if (!player.slowingDown)
            StartCoroutine(DoSlowDown());
    }

    IEnumerator DoSlowDown()
    {
        player.SetSlowDown(true);
        GetComponent<Image>().color = new Color(1, 1, 1); // 道具按钮激活时还原为彩色
        text.fontSize = 72;
        float waittime = 0.1f;
        while (player.slowdownTime > 0)
        {
            text.text = ((int)player.slowdownTime + 1).ToString() + "s";
            yield return new WaitForSeconds(waittime);
            player.slowdownTime -= waittime;
        }
        text.fontSize = 45;
        //text.text = "Slow Down"; 
        text.text = ""; // 清除读秒
        player.SetSlowDown(false);
        GetComponent<Image>().color = new Color(0.3f, 0.4f, 0.6f); // 激活结束后按钮置灰
    }

}
