using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrowControl : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float f;
    public float duration;
    public List<ArrowUnit> listArrowDisable;
    public List<ArrowUnit> listArrowActive;
    ArrowUnit currentArrow;
    Vector3[] listDirect;
    Vector3 left, right, foward;
    bool roting;
    public bool isGameOver = false;

    bool isDirect0;

    void Awake()
    {
        listDirect = new Vector3[2];
        left = new Vector3(0, 0, 0);
        right = new Vector3(0, 180, 0);
        foward = new Vector3(0, 90, 0);
    }

    public void SetArrow(ArrowUnit arrow)
    {
        listArrowDisable.Add(arrow);
    }


    ArrowUnit GetArrow()
    {
        if (listArrowDisable.Count == 0)
        {
            return (Instantiate(arrowPrefab) as GameObject).GetComponent<ArrowUnit>();
        }
        else
        {
            ArrowUnit arrow = listArrowDisable[0];
            listArrowDisable.RemoveAt(0);
            return arrow;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGameOver && MainState.GetState == MainState.State.Ingame)
        {
            if (!roting)
            {
                if (isDirect0)
                {
                    currentArrow.StartRotate(listDirect[0], listDirect[1]);
                }
                else
                {
                    currentArrow.StartRotate(listDirect[1], listDirect[0]);
                }
                isDirect0 = !isDirect0;
                MainAudio.Main.PlaySound(TypeAudio.SoundDown);
            }
        }
    }

    void CheckArrow()
    {
        if (currentArrow != null)
        {
            listArrowActive.Add(currentArrow);
        }

        if (listArrowActive.Count > 1)
        {
            Vector3 downPos = listArrowActive[0].transform.position;
            StartCoroutine(Move(listArrowActive[0], new Vector3(downPos.x, -5.5f, downPos.z), 1, true));
            listArrowActive.RemoveAt(0);
        }
    }

    IEnumerator Move(ArrowUnit unit, Vector3 to, float duration, bool isSet)
    {
        float elapsed = 0;
        Vector3 from = unit.transform.position;

        while (elapsed <= duration)
        {
            unit.transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (isSet)
        {
            listArrowDisable.Add(unit);
        }

        unit.transform.position = to;
    }


    public void MoveUpArrow()
    {
        Vector3 upPos = currentArrow.transform.position;
        StartCoroutine(Move(currentArrow, new Vector3(upPos.x, 0, upPos.z), 0.5f, false));
    }

    bool isFirst = true;

    public void SetNewArrow(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        CheckArrow();
        currentArrow = GetArrow();
        Vector3 pos;
        if (p1.z == p3.z)
        {
            pos = Vector3.back * f;
            if (p1.x > p3.x)
            {
                listDirect[0] = foward;
                listDirect[1] = left;
            }
            else
            {
                listDirect[0] = foward;
                listDirect[1] = right;
            }
        }
        else if (p1.x == p3.x)
        {
            pos = Vector3.right * f;
            listDirect[0] = foward;
            listDirect[1] = left;
        }
        else
        {
            if (p1.x == p2.x)
            {
                if (p2.x < p3.x)
                {
                    pos = Vector3.left * f;
                    listDirect[0] = foward;
                    listDirect[1] = right;
                }
                else
                {
                    pos = Vector3.right * f;
                    listDirect[0] = foward;
                    listDirect[1] = left;
                }
            }
            else
            {
                pos = Vector3.back * f;
                if (p1.x < p2.x)
                {
                    listDirect[0] = foward;
                    listDirect[1] = right;
                }
                else
                {
                    listDirect[0] = foward;
                    listDirect[1] = left;
                }
            }
        }

        if (isFirst)
        {
            currentArrow.transform.position = p2;
            isFirst = false;
        }
        else
        {
            currentArrow.transform.position = new Vector3(p2.x, -5.5f, p2.z);
        }

       
        currentArrow.arrow.localPosition = new Vector3(pos.x, 2.81f, pos.z);

        int rand = Random.Range(0, 2);
        currentArrow.arrow.transform.eulerAngles = listDirect[rand];
        currentArrow.bridgeR.transform.eulerAngles = listDirect[rand];
        if (rand == 0)
        {
            isDirect0 = true;
        }
        else
        {
            isDirect0 = false;
        }
            
    }

   

    public Vector3 GetDirect()
    {
        Vector3 thisDirect;
        if (isDirect0)
        {
            thisDirect = listDirect[0];
        }
        else
        {
            thisDirect = listDirect[1];
        }

        if (thisDirect.y == 0)
        {
            return Vector3.left;
        }
        else if (thisDirect.y == 180)
        {
            return Vector3.right;
        }
        else
        {
            return Vector3.forward;
        }
    }

    public ArrowUnit CreateFork(Vector3 forkPos, GameDefine.Direction pathDir)
    {
        GameDefine.Direction[] turns = new GameDefine.Direction[2];

        if (pathDir == GameDefine.Direction.Forward)
        {
            turns[0] = GameDefine.Direction.Forward;
            turns[1] = Random.value > 0.5 ? GameDefine.Direction.Right : GameDefine.Direction.Left;
        }
        else
        {
            turns[0] = pathDir;
            turns[1] = GameDefine.Direction.Forward;
        }

        // TODO: Create Fork
        ArrowUnit arrow = GetArrow();
        arrow.SetPosition(forkPos, pathDir, turns);
        arrow.directions = turns;

        return arrow;
    }
}
