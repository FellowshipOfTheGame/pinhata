using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private float TimeBetweenWaves = 3f;
    [SerializeField]
    private UIPlayerHealth[] UIPlayerHealth;

    public static GameManager Instance;

    public CameraFollowComponent cameraFollow;
    public GameObject[] players;

    public GameObject pausePanel;

    private int players_alive_count;
    private int level;
    private bool startSpawn;
    private float timer;

    private void Awake() {
        if (Instance == null)
            Instance = this;

        players_alive_count = 0;
        timer = 0f;
        startSpawn = false;
        level = 0;

        //Instantiate players
        if (!FindObjectOfType<DeviceManager>().InstantiatePlayers()) {
            Debug.Log("Loading Error, NOT ENOUGH PLAYERS!");
            return;
        }

        //Enable and setup Health UI
        players = GameObject.FindGameObjectsWithTag("Player");
        //Set alive count
        players_alive_count = players.Length;

        for (int i = 0; i < players.Length; i++) {
            UIPlayerHealth[i].Setup(players[i]);
            UIPlayerHealth[i].transform.parent.gameObject.SetActive(true);
            //Set ActionMap
            players[i].GetComponent<PlayerInput>().SwitchCurrentActionMap("Player Controls");
        }

        //Camera Follow Setup
        CameraFollowSetup(0);

        //Debug.Log(players.Length);
    }

    private void CameraFollowSetup(int index) {
        cameraFollow.Setup(() => players[index] != null ? players[index].transform.position : Vector3.zero);
    }

    private void Update() {
        ChargeLevel();
    }

    private void ChargeLevel() {
        if (!EnemyManager.Instance.AreEnemiesAlive()) {
            timer += Time.deltaTime;

            if (!startSpawn && timer >= TimeBetweenWaves) {
                startSpawn = true;
                timer = 0f;
                level++;
                EnemyManager.Instance.SetSpawn(level);
                startSpawn = false;
            }
        }
    }

    public void OnPlayerDeath() {
        players_alive_count--;

        if (players_alive_count <= 0)
            Reload();

        //Mudar jogador sendo seguido pela camera
        for (int i = 0; i < players.Length; i++) {
            if (players[i] == null) continue;
            if (players[i].activeSelf) {
                CameraFollowSetup(i);
                break;
            }
        }
    }

    public void Reload() {
        DeviceManager.Reload();
        SceneManager.LoadScene("Stats");
    }

    public void Pause() {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void Continue() {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}