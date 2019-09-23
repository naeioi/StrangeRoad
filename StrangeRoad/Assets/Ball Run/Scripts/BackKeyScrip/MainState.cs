using UnityEngine;
using System.Collections;

public class MainState
{

    public enum State
    {
        Home,
        Ingame,
        GameOver,
        Pause,
        Waiting,
    }

    public static State state;
    public static bool allowButton;

    public static void SetState(State newState)
    {
        state = newState;
    }

    public static State GetState
    {
        get { return state; }
    }
}


