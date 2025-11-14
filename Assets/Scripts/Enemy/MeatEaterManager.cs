using System;
using UnityEngine;
using System.Collections;

public class MeatEaterManager
{
    private float timer;
    private Timer timerClass;

    public MeatEaterManager(Timer t)
    {
        timerClass = t;
        timer = 0;
    }

    public void SetTimer(float x)
    {
        timer += x;
        TimerChanged?.Invoke(timer);
        if (timer >= 60f)
        {
            OnBeastInvoked?.Invoke();
        }
    }
    public void ResetTimer()
    {
        timer = 0f;
    }

    public void AddBeast()
    {
        OnBeastInvoked?.Invoke();
    }

    public event Action<float> TimerChanged;
    public event Action OnBeastInvoked;
}
