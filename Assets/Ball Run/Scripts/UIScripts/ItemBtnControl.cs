using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ItemBtnControl : MonoBehaviour
{
    public delegate void ItemFinishAction();

    public int effectiveDuration;
    public int price;

    protected TextMeshProUGUI text;
    protected GameObject priceUI;

    protected PlayerControl player
    {
        get
        {
            return MainObjControl.Instant.playerCtrl;
        }
    }

    private void Start()
    {
        GetComponent<Image>().color = new Color(0.3f, 0.4f, 0.6f); // 初始按钮置灰
        text = GetComponentInChildren<TextMeshProUGUI>();
        priceUI = GetComponentsInChildren<Transform>().Where(r => r.tag == "ItemPrice").FirstOrDefault().gameObject;
        priceUI.GetComponentInChildren<Text>().text = price.ToString();

    }

    protected void play(ItemFinishAction callback)
    {
        StartCoroutine(DoItem(callback));
    }

    IEnumerator DoItem(ItemFinishAction callback)
    {
        GetComponent<Image>().color = new Color(1, 1, 1); // 道具按钮激活时还原为彩色

        priceUI.SetActive(false);

        text.fontSize = 72;
        float waittime = 0.1f, countdown = effectiveDuration;
        while (countdown > 0)
        {
            text.text = ((int)countdown + 1).ToString() + "s";
            yield return new WaitForSeconds(waittime);
            countdown -= waittime;
        }
        text.fontSize = 45;
        
        text.text = "";  // 清除读秒
        GetComponent<Image>().color = new Color(0.3f, 0.4f, 0.6f); // 激活结束后按钮置灰

        priceUI.SetActive(true);
        callback();
    }
}
