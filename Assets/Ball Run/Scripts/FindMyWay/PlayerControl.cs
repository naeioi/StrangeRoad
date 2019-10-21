using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    public float speedR;
    public Material playerMat;
    public Color c1;
    public Color c2;
    public float duration;
    Vector3 startPoint;
    Vector3 currentPos;
    Vector3 targetPos;
    bool currentType;
    bool isFirst = true;
    IEnumerator IEGo;
    public bool isGameover = false;
    public bool isTest;

    void Awake()
    {
        speed = 5;
        startPoint = new Vector3(0, transform.position.y, 0);
        targetPos = MainObjControl.Instant.roadCrt.listPointActive[0] + startPoint;
        currentType = MainObjControl.Instant.roadCrt.listType[0];
        HardChangeColor(currentType);
        isGameover = false;
    }

    public void Run()
    {
        StartCoroutine(Go());
    }

    IEnumerator Go()
    {
        while (!isGameover)
        {
            currentPos = targetPos;
            targetPos = MainObjControl.Instant.roadCrt.listPointActive[1] + startPoint;
            if (isFirst)
            {
                isFirst = false;
            }
            else
            {
                if (!isTest)
                {
                    CheckGameOver(currentPos, targetPos);
                }
            }

            if (!isGameover)
            {
                StartCoroutine(ChangeColor(MainObjControl.Instant.roadCrt.listType[1]));
                MainObjControl.Instant.camCrt.NewTarget(targetPos);
                MainObjControl.Instant.roadCrt.PlayerCall();


                transform.eulerAngles = Vector3.zero;
                float duration;
                Vector3 direct;
                if (currentPos.x == targetPos.x)
                {
                    duration = Mathf.Abs(currentPos.z - targetPos.z) / speed;
                    direct = Vector3.right;
                }
                else
                {
                    duration = Mathf.Abs(currentPos.x - targetPos.x) / speed;
                    if (currentPos.x > targetPos.x)
                    {
                        direct = Vector3.forward;

                    }
                    else
                    {
                        direct = Vector3.back;
                    }

                }

                float timer = 0;

                while (timer < duration)
                {
                    timer += Time.deltaTime;
                    transform.position = Vector3.Lerp(currentPos, targetPos, timer / duration);
                    transform.Rotate(direct * speedR * Time.deltaTime);
                    yield return null;
                }
            }
        }

    }

    void CheckGameOver(Vector3 pStart, Vector3 pEnd)
    {
        Vector3 arrowDirect = MainObjControl.Instant.arrowCrt.GetDirect();
        if (arrowDirect != (pEnd - pStart).normalized)
        {
            isGameover = true;
           
            MainObjControl.Instant.arrowCrt.isGameOver = true;
            StartCoroutine(GoDie());
        }
        else
        {
            MainCanvas.Main.inGameScript.UpScoreTxt();
        }
    }

    IEnumerator GoDie()
    {
        float timer = 0;
        float distcane = GameDefine.blockDistance - 0.6f;
        duration = distcane / speed;

        Vector3 startPos = currentPos;
        Vector3 endPos = MainObjControl.Instant.roadCrt.GetEndPos();
        Vector3 newEndPos;

        Vector3 direct;
        if (startPos.x == endPos.x)
        {
            direct = Vector3.right;
            newEndPos = Vector3.forward;
        }
        else
        {
            if (startPos.x > endPos.x)
            {
                direct = Vector3.forward;
                newEndPos = Vector3.left;
            }
            else
            {
                direct = Vector3.back;
                newEndPos = Vector3.right;
            }
        }

        newEndPos = newEndPos * distcane + startPos;
        transform.eulerAngles = Vector3.zero;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, newEndPos, timer / duration);
            transform.Rotate(direct * speedR * Time.deltaTime);
            yield return null;
        }
        transform.position = newEndPos;
        MainAudio.Main.PlaySound(TypeAudio.SoundStop);
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        bool isleft;

        if (direct == Vector3.back || direct == Vector3.forward)
        {
            isleft = false;
        }
        else
        {
            isleft = true;
        }
        Material blockMat = MainObjControl.Instant.roadCrt.GetEndMat();

        MainObjControl.Instant.boomControl.ShowPlayerBoom(isleft, blockMat);

        yield return new WaitForSeconds(0.1f);
        MainCanvas.Main.lostScript.GameOver();
    }

    IEnumerator ChangeColor(bool isType1)
    {
        float timer = 0;
        BlockUnit unit = MainObjControl.Instant.roadCrt.listBlock[0];
        if (currentType != isType1)
        {
            yield return new WaitForSeconds(0.4f);


            MainObjControl.Instant.colorBlockCrt.SetBlock(unit);
            MainObjControl.Instant.boomControl.ShowBoom(unit.transform);
            MainAudio.Main.PlaySound(TypeAudio.SoundScore);
            unit.transform.position = new Vector3(0, -100, 0);
            while (timer < duration)
            {
                timer += Time.deltaTime;
                Color newColor;

                if (isType1)
                {
                    newColor = Color.Lerp(c2, c1, timer / duration);
                }
                else
                {
                    newColor = Color.Lerp(c1, c2, timer / duration);
                }
                playerMat.SetColor("_Color", newColor);
                yield return null;
            }

            currentType = isType1;
        }
        else
        {
            yield return new WaitForSeconds(0.4f);

            MainObjControl.Instant.colorBlockCrt.SetBlock(unit);
            MainObjControl.Instant.boomControl.ShowBoom(unit.transform);
            unit.transform.position = new Vector3(0, -100, 0);
            MainAudio.Main.PlaySound(TypeAudio.SoundScore);
        }
    }

    void HardChangeColor(bool isType1)
    {
        if (isType1)
        {
            playerMat.SetColor("_Color", c1);
        }
        else
        {
            playerMat.SetColor("_Color", c2);
        }
    }
}
