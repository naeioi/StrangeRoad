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

    protected bool playing;
    protected TextMeshProUGUI text;
    protected GameObject priceUI;
    protected float countdown;

    protected PlayerControl player
    {
        get
        {
            return MainObjControl.Instant.playerCtrl;
        }
    }

    protected bool available
    {
        get
        {
            return MainCanvas.Instance.inGameScript.scoreInt * 100 >= price;
        }
    }

    protected enum State { Unavailable, Available, Active };
    protected State state
    {
        get
        {
            if (playing) return State.Active;
            else return available ? State.Available : State.Unavailable;
        }
    }

    void updateColor()
    {
        var image = GetComponent<Image>();
        if (state == State.Unavailable)
            image.color = new Color(0.3f, 0.4f, 0.6f);
        else if (state == State.Available)
            image.color = new Color(1, 1, 1);
        else if (state == State.Active)
            image.color = new Color(1, 0.5f, 0.5f);
    }

    private void Start()
    {
        updateColor();
        text = GetComponentInChildren<TextMeshProUGUI>();
        priceUI = GetComponentsInChildren<Transform>().Where(r => r.tag == "ItemPrice").FirstOrDefault().gameObject;
        priceUI.GetComponentInChildren<Text>().text = price.ToString();
        MainCanvas.Instance.inGameScript.scoreChangeListeners.Add(value => updateColor());
    }

    protected void Play(ItemFinishAction callback)
    {
        StartCoroutine(DoItem(callback));
    }

    void WalletUpdated()
    {
        if (playing) return;
        updateColor();
    }

    IEnumerator DoItem(ItemFinishAction callback)
    {
        playing = true;
        updateColor();

        priceUI.SetActive(false);

        text.fontSize = 72;
        countdown = effectiveDuration;
        float waittime = 0.1f;
        while (countdown > 0)
        {
            text.text = ((int)countdown + 1).ToString() + "s";
            yield return new WaitForSeconds(waittime);
            countdown -= waittime;
        }
        text.fontSize = 45;
        
        text.text = "";  // 清除读秒
        priceUI.SetActive(true);
        playing = false;
        updateColor();

        callback();
    }
}
