using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class DiamonBtnControl : ItemBtnControl
{
    int diamondCount
    {
        get => MainCanvas.Instance.inGameScript.diamondCount;
        set => MainCanvas.Instance.inGameScript.diamondCount = value;
    }

    protected override bool available
    {
        get
        {
            return diamondCount > 0;
        }
    }

    private void Start()
    {
        updateColor();
        countdownText = GetComponentInChildren<TextMeshProUGUI>();
        explainUI = GetComponentsInChildren<Transform>().Where(r => r.tag == "ItemPrice").FirstOrDefault().gameObject;
        explainUI.GetComponentInChildren<Text>().text = "x " + diamondCount.ToString();
    }

    public void DiamondCountUpdated()
    {
        if (playing) return;
        updateColor();
        if (explainUI)
            explainUI.GetComponentInChildren<Text>().text = "x " + diamondCount.ToString();
    }
    
    public void OnDiamond()
    {
        diamondCount--;
        MainCanvas.Instance.inGameScript.isDoublingScore = true;
        Play(() => MainCanvas.Instance.inGameScript.isDoublingScore = false);
    }

}
