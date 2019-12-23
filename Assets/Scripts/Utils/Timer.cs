using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate void CooldownEndedDelegate();

    private float cooldown;
    private float timePassed = 0f;
    private bool isTimeRunning = false;
    public CooldownEndedDelegate onCooldownEnded;

    private void Awake()
    {
        StopTimer();
        ResetTimer();
    }

    public void StartTimer()
    {
        isTimeRunning = true;
    }

    public void StopTimer()
    {
        isTimeRunning = false;
    }

    public void SetCooldown(float cooldown)
    {
        this.cooldown = cooldown;
    }

    public void ResetTimer()
    {
        timePassed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeRunning)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= cooldown)
            {
                onCooldownEnded?.Invoke();
                StopTimer();
            }
        }
    }
}
