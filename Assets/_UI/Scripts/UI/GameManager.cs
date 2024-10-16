﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
public enum GameState{
    MainMenu,
    GamePlay,
    Win,
    Lose,
}
public class GameManager : Singleton<GameManager>
{
    public Camera cameraMain;
    private static GameState gameState = GameState.MainMenu;

    public ConfigTankSO configTank;

    // Start is called before the first frame update
    protected void Awake()
    {
        //base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        ChangeState(GameState.MainMenu);

        UIManager.Ins.OpenUI<MianMenu>();
    }
    public static void ChangeState(GameState state)
    {
       gameState = state;
    }

    public static bool IsState(GameState state)
    {
       return gameState == state;
    }
  
}
