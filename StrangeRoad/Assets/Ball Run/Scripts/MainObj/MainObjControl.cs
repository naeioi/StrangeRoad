using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class MainObjControl : MonoBehaviour
{
    static MainObjControl main;
    public ColorControl colorControl;
 
    public BoomControl boomControl;

    public RoadControl roadCrt;
    public PlayerControl playerCrt;
    public CameraController camCrt;
    public ColorBlockControl colorBlockCrt;
    public ArrowControl arrowCrt;

    void Awake()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;

        main = this;
    }

    public static MainObjControl Instant
    {
        get { return main; }
    }
}
