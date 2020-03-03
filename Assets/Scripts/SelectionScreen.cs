using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnLocations = new GameObject[4];
    private DeviceManager deviceManager;
    private PlayerUIController[] players;

    private void Awake()
    {
        players = new PlayerUIController[4];
        deviceManager = GetComponent<DeviceManager>();
        deviceManager.playerJoined += OnPlayerJoin;
        deviceManager.playerLeft += OnPlayerLeft;
    }


    private void OnPlayerJoin(GameObject player, int index)
    {
        player.GetComponent<Rigidbody>().isKinematic = true;

        if (spawnLocations[index] != null)
        {
            spawnLocations[index].SetActive(true);
        }

        players[index] = player.GetComponent<PlayerUIController>();
        players[index].playerStarted += OnPlayerStarted;
        players[index].playerCanceled += OnPlayerCanceled;
    }

    private void OnPlayerCanceled(GameObject player)
    {
        Destroy(player, 0.5f);
    }

    private void OnPlayerStarted()
    {
        SceneManager.LoadScene("PlayerScene");
    }

    private void OnPlayerLeft(GameObject player, int index)
    {
        if (spawnLocations[index] != null)
        {
            spawnLocations[index].SetActive(false);
        }

        players[index].playerStarted -= OnPlayerStarted;
        players[index].playerCanceled -= OnPlayerCanceled;
        players[index] = null;
    }

    
}
