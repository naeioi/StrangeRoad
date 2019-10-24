using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    public float speedR;
    public float speedOrient;
    public Material playerMat;
    public Color c1;
    public Color c2;

    public GameDefine.Direction direction;

    public float duration;
    public Vector3 startPoint;
    Vector3 currentPos;
    Vector3 targetPos;
    bool currentType;
    bool isFirst = true;
    IEnumerator IEGo;
    public bool isGameover = false;
    public bool isTest;

    public RoadUnit currentRoad;
    public Rigidbody rigidBody;
    Vector3 targetOrient;
    public bool running;

    Vector3 velocity
    {
        get
        {
            // return Vector3.zero;
            return running ? speed * MovementsByDirection[(int)direction] : Vector3.zero;
        }
    }

    Quaternion orientation
    {
        get
        {
            return OrientByDirection[(int)direction];
        }
    }

    Vector3 angularVelocity
    {
        get {
            return running? speedR * RotationsByDirection[(int)direction] : Vector3.zero;
        }
}

    readonly Vector3[] MovementsByDirection =
    {
        Vector3.left,
        Vector3.right,
        Vector3.forward
    };

    readonly Vector3[] RotationsByDirection =
    {
        Vector3.back,
        Vector3.back,
        Vector3.right
    };

    readonly Quaternion[] OrientByDirection =
    {
        Quaternion.Euler(0, -90, 0),
        Quaternion.Euler(0, 90, 0),
        Quaternion.Euler(0, 0, 0)
    };

    void Awake()
    {
        isGameover = false;
    }

    private void Start()
    {
        Run();
    }

    public void Stop()
    {
        running = false;
        rigidBody.angularVelocity = Vector3.zero;
    }

    public void SetDirection(GameDefine.Direction direction)
    {
        this.direction = direction;
        rigidBody.angularVelocity = angularVelocity;
    }

    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, orientation, Time.deltaTime * speedOrient);
    }

    public void Run()
    {
        running = true;
        SetDirection(direction);
    }

    void CheckGameOver(Vector3 pStart, Vector3 pEnd)
    {
        Vector3 arrowDirect = MainObjControl.Instant.arrowCtrl.GetDirect();
        if (arrowDirect != (pEnd - pStart).normalized)
        {
            isGameover = true;
           
            MainObjControl.Instant.arrowCtrl.isGameOver = true;
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
        Vector3 endPos = MainObjControl.Instant.roadCtrl.GetEndPos();
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
        Material blockMat = MainObjControl.Instant.roadCtrl.GetEndMat();

        MainObjControl.Instant.boomCtrl.ShowPlayerBoom(isleft, blockMat);

        yield return new WaitForSeconds(0.1f);
        MainCanvas.Main.lostScript.GameOver();
    }

    IEnumerator ChangeColor(bool isType1)
    {
        float timer = 0;
        BlockUnit unit = MainObjControl.Instant.roadCtrl.listBlock[0];
        if (currentType != isType1)
        {
            yield return new WaitForSeconds(0.4f);


            MainObjControl.Instant.blockCtrl.FreeBlock(unit);
            MainObjControl.Instant.boomCtrl.ShowBoom(unit.transform);
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

            MainObjControl.Instant.blockCtrl.FreeBlock(unit);
            MainObjControl.Instant.boomCtrl.ShowBoom(unit.transform);
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
