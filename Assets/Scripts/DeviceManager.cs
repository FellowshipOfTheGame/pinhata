using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class DeviceManager : MonoBehaviour
{
    //Statics
    [HideInInspector]
    public static InputDevice[] PlayerDevices { get; } = new InputDevice[4];

    private static int PlayerCount = 0;

    //Events
    public event Action<GameObject, int> playerJoined;
    public event Action<GameObject, int> playerLeft;

    [SerializeField]
    private GameObject playerPrefab = null;

    public bool IsLeavingScene { get;  set; } = false;

    public void InstantiatePlayers()
    {
        for(int i = 0; i < 4; i++)
        {
            if(PlayerDevices[i] != null)
            {
                PlayerInputManager pm = GetComponent<PlayerInputManager>();
                pm.playerPrefab = playerPrefab;
                pm.JoinPlayer(pairWithDevice: PlayerDevices[i]);
                Debug.Log("Joining player");
            }
        }
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        int ind = playerInput.playerIndex;
        Debug.Log("Joined" + ind);
        PlayerDevices[ind] = playerInput.devices[0];
        PlayerCount++;

        playerJoined?.Invoke(playerInput.gameObject, ind);
    }

    void OnPlayerLeft(PlayerInput playerInput)
    {
        if (IsLeavingScene) return;

        int ind = playerInput.playerIndex;
        playerLeft?.Invoke(playerInput.gameObject, ind);

        PlayerDevices[ind] = null;
        Debug.Log("Left" + ind);
    }
}
