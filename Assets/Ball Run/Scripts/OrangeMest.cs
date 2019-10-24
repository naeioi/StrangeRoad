using UnityEngine;
using System.Collections;

public class OrangeMest : MonoBehaviour
{
    public Transform[] listTrans;
    public Material boomMat;
    public float duration;
    public float speedMin, speedMax;
    float speedY;
    Vector3 scale = new Vector3(0.25f, 0.33f, 1);
    Vector3 R = new Vector3(0, 90, 0);

    public void Boom(Transform trans)
    {
        transform.position = trans.position;
        if (trans.localScale.x == 1.2f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = R;
        }
       
        Reset(trans.GetComponent<MeshRenderer>().material);
    }

    public void PlayerBoom(bool isLeft, Material blockMat)
    {
        transform.position = MainObjControl.Instant.playerCtrl.transform.position;
        transform.localScale = new Vector3(0.8f, 0.8f, 0.3f);

        if (isLeft)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = R;
        }

        Reset(blockMat);
    }

    void Reset(Material blockMat)
    {
        boomMat.SetColor("_FrontColor", blockMat.GetColor("_FrontColor"));
        boomMat.SetColor("_TopColor", blockMat.GetColor("_TopColor"));
        boomMat.SetColor("_LeftColor", blockMat.GetColor("_LeftColor"));

        int index = 0;
        float startX = -0.125f * 3;
        float startY = -0.33f;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Vector3 localPosl = new Vector3(startX, startY, 0);
                listTrans[index].localPosition = localPosl;
                listTrans[index].localScale = scale;

                StartCoroutine(Jump(listTrans[index], localPosl));
                startX += 0.25f;
                index++;
            }
            startX = -0.125f * 3;
            startY += 0.33f;
        }

        StartCoroutine(WaitReturn());
    }

    IEnumerator WaitReturn()
    {
        yield return new WaitForSeconds(duration * 1.2f);
        SetBoom();
    }

    void SetBoom()
    {
        MainObjControl.Instant.boomCtrl.SetBoom(gameObject);
    }

    IEnumerator Jump(Transform box, Vector3 direct)
    {
        float timeJumpCounter = 0;
        float speed = Random.Range(speedMin, speedMax);
        direct = direct.normalized;
        while (timeJumpCounter < duration)
        {
            timeJumpCounter += Time.deltaTime;
            box.localPosition += speed * direct * Time.deltaTime;
            box.localScale = Vector3.Lerp(scale, Vector3.zero, timeJumpCounter / duration);
            yield return null;
        }
    }

    float GetSpeedZ(float timeJumpCounter, float JumpHeight, float JumpTime)
    {
        return (2 * JumpHeight / JumpTime) - (2 * JumpHeight / (JumpTime * JumpTime)) * timeJumpCounter;
    }
}
