using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField]
    public float lifeTime = 0f;

    private Timer timer;

    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.onCooldownEnded += AutoDestroy;
        timer.SetCooldown(lifeTime);
        timer.StartTimer();
    }

    private void AutoDestroy()
    {
        Destroy(gameObject);
    }
}
