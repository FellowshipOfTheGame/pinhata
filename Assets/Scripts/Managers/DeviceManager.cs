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
    //[HideInInspector]
    public static InputDevice[] PlayerDevices { get; } = new InputDevice[4];

    private static int PlayerCount = 0;

    //Events
    public event Action<GameObject, int> playerJoined;
    public event Action<GameObject, int> playerLeft;

    [SerializeField]
    private GameObject playerPrefab = null;

    public Vector3[] spawnPositions;
    public bool IsLeavingScene { get;  set; } = false;

    public bool InstantiatePlayers()
    {
        //Debug.Log("Calling InstantiatePlayers");
        int joined_players = 0;
        Vector3 pos = Vector3.zero;
        for(int i = 0; i < 4; i++)
        {
            if(PlayerDevices[i] != null)
            {
                PlayerInputManager pm = GetComponent<PlayerInputManager>();
                pm.playerPrefab = playerPrefab;
                pm.JoinPlayer(pairWithDevice: PlayerDevices[i]).transform.position = spawnPositions[i];
                joined_players++;
            }
        }
        return joined_players > 0;
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        int ind = playerInput.playerIndex;
        //Debug.Log("Joined" + ind);
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

    public static void Reload()
    {
        for(int i = 0; i < PlayerDevices.Length; i++)
        {
            PlayerDevices[i] = null;
        }
        Debug.Log("Reloading DeviceManager");
    }
}
