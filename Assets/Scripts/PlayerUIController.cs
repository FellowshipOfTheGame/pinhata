using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerUIController : MonoBehaviour
{
    public event Action playerStarted;
    public event Action<GameObject> playerCanceled;

    void OnCancel()
    {
        Debug.Log("Cancel event");
        playerCanceled?.Invoke(gameObject);
    }

    void OnJoin()
    {
        Debug.Log("Start event");
        playerStarted?.Invoke();
    }
}
