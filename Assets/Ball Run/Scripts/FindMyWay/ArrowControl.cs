using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class ArrowControl : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float f;
    public float duration;
    public List<ArrowUnit> listArrowDisable;
    public List<ArrowUnit> listArrowActive;
    Vector3[] listDirect;
    Vector3 left, right, foward;
    bool roting;
    public bool isGameOver = false;

    bool isDirect0;

    public ArrowUnit currentArrow
    {
        get
        {
            return MainObjControl.Instant.playerCtrl.currentRoad.arrow;
        }
    }

    void Awake()
    {
        listDirect = new Vector3[2];
        left = new Vector3(0, 0, 0);
        right = new Vector3(0, 180, 0);
        foward = new Vector3(0, 90, 0);
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
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !isGameOver && MainState.GetState == MainState.State.Ingame)
        // if (Input.GetMouseButtonDown(0))
        {
            if (!roting)
            {
                currentArrow.StartToggle();
                MainAudio.Main.PlaySound(TypeAudio.SoundDown);
            }
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

    public ArrowUnit CreateArrow(Vector3 forkPos, GameDefine.Direction pathDir)
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
