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
    private List<PlayerUIController> players;

    private void Awake()
    {
        players = new List<PlayerUIController>();
        players.Capacity = 4;
        deviceManager = GetComponent<DeviceManager>();
        deviceManager.playerJoined += PlayerJoined;
        deviceManager.playerLeft += PlayerLeft;
    }


    private void PlayerJoined(GameObject player, int index)
    {
        player.GetComponent<Rigidbody>().isKinematic = true;

        if (spawnLocations[index] != null)
        {
            spawnLocations[index].SetActive(true);
        }
        
        players.Insert(index, player.GetComponent<PlayerUIController>());
        players[index].playerCanceled += OnPlayerCanceled;
        //players[index] = player.GetComponent<PlayerUIController>();
    }

    private void OnPlayerCanceled(GameObject player)
    {
        player.SetActive(false);
        Destroy(player, 0.2f);
    }

    private void PlayerLeft(GameObject player, int index)
    {
        if (spawnLocations[index] != null)
        {
            spawnLocations[index].SetActive(false);
        }
        
        players[index].playerCanceled -= OnPlayerCanceled;
        players.RemoveAt(index);
        //players[index] = null;
    }

    public void LoadNextScene()
    {
        if (players.Count > 0)
        {
            deviceManager.IsLeavingScene = true;
            SceneManager.LoadScene("PlayerScene");
        }
        else Debug.Log("Not enough players");
    }

    
}
