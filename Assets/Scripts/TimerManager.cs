﻿using System;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private static float maxTime = 300f;
    private static float time;

    public static Action timerAction;



    private void Update()
    {
        timerAction?.Invoke();
    }

    public static void Timer()
    {
        time -= Time.deltaTime;
        if (time <= 0) LoseManager.Lose();
    }
    public static void StartTimer()
    {
        time = maxTime;
        timerAction += Timer;
    }
    public static void StopTimer()
    {
        timerAction -= Timer;
    }
}