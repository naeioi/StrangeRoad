﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class MainObjControl : MonoBehaviour
{
    static MainObjControl main;
    public ColorControl colorCtrl;
 
    public BoomControl boomCtrl;

    public RoadControl roadCtrl;
    public PlayerControl playerCtrl;
    public CameraControl camCtrl;
    public BlockControl blockCtrl;
    public ArrowControl arrowCtrl;

    void Awake()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;

        main = this;

        Random.seed = 1;
    }

    public static MainObjControl Instant
    {
        get { return main; }
    }
}
