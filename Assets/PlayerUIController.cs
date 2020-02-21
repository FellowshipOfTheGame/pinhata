using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerUIController : MonoBehaviour
{
    public event Action playerJoined;
    public event Action playerCanceled;

    void OnCancel()
    {
        Debug.Log("Cancel event");
        playerCanceled?.Invoke();
        SceneManager.LoadScene("PlayerScene");
    }

    void OnJoin()
    {
        Debug.Log("OnJoin event");
        playerJoined?.Invoke();
    }
}
