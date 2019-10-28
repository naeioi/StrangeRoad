using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class ArrowControl : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float f;
    public float duration;
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
    }

    ArrowUnit GetArrow()
    {
        return (Instantiate(arrowPrefab) as GameObject).GetComponent<ArrowUnit>();
    }
    
    bool isPointerOnUI()
    {
        return EventSystem.current.IsPointerOverGameObject() ||
            (Input.touchCount > 0 && 
             Input.touches[0].phase == TouchPhase.Began && 
             EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPointerOnUI() && !isGameOver && MainState.GetState == MainState.State.Ingame)
        // if (Input.GetMouseButtonDown(0))
        {
            if (!roting)
            {
                currentArrow.StartToggle();
                MainAudio.Main.PlaySound(TypeAudio.SoundDown);
            }
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
