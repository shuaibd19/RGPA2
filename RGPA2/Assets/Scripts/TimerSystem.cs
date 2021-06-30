using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    bool timerIsRunning = false;
    float secondsLeft;
    void Update()
    {
        if (timerIsRunning)
        {
            secondsLeft -= Time.deltaTime;
            if (secondsLeft <= 0)
                StopTimer();
        }
    }


    void StartTimer(float seconds)
    {
        secondsLeft = seconds;
        timerIsRunning = true;
    }

    private bool StopTimer()
    {
        timerIsRunning = false;
        return false;
    }

    //Enter a positive value to add, negative to decrease
    void AddSubtractTime(float timeToAdd)
    {
        secondsLeft += timeToAdd;
    }
}
