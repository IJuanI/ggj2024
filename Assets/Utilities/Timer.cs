using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Timer
{
    [SerializeField] bool timerStarted;
    [SerializeField] float currentTime;
    [SerializeField] float timeToGoOff;

    public Timer(bool _timerStarted, float _currentTime,float _timeToGoOff)
    {
        timerStarted = _timerStarted;
        currentTime = _currentTime;
        timeToGoOff = _timeToGoOff;
    }

    public bool TimerStarted { get => timerStarted; set => timerStarted = value; }
    public float CurrentTime { get => currentTime; set => currentTime = value; }
    public float TimeToGoOff { get => timeToGoOff; set => timeToGoOff = value; }

    public event Action OnFinishTimer;
    
    public void UpdateTimer()
    {
        if (TimerStarted)
        {
            if (CurrentTime>=TimeToGoOff)
            {
                OnFinishTimer?.Invoke();
            }
            else
            {
                CurrentTime += Time.deltaTime;
            }

        }
    }
    public void StartTimer(float _time)
    {
        timeToGoOff = _time;
        timerStarted = true;
        currentTime = 0;
        Debug.Log("timer to go off:" + timeToGoOff);
    }
}
