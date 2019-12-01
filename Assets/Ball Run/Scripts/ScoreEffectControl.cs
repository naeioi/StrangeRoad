using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreEffectControl : MonoBehaviour
{
    public GameObject textObj;
    TextMeshPro textPro;

    private void Start()
    {
        textPro = textObj.GetComponent<TextMeshPro>();
    }

    public void Play(Vector3 pos, GameDefine.Direction dir, string text, Color? color = null)
    {
        textPro.color = color.GetValueOrDefault(new Color(0.3679245f, 0.3679245f, 0.3679245f));

        pos.y = 1f;

        pos += 2f * (dir == GameDefine.Direction.Forward ? Vector3.forward :
            dir == GameDefine.Direction.Left ? Vector3.left : Vector3.right);
        textObj.transform.position = pos;
        textObj.transform.eulerAngles =
            dir == GameDefine.Direction.Forward ? Vector3.zero :
            dir == GameDefine.Direction.Left ? new Vector3(0, -90f, 0) : new Vector3(0, 90f, 0);

        textPro.text = text;
        textPro.alpha = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y += 5 * Time.deltaTime * (2f - pos.y);
        transform.position = pos;

        float alpha = textPro.alpha;
        alpha +=  2.5f * Time.deltaTime * (0 - alpha);
        textPro.alpha = alpha;
    }
}
