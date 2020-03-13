using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private float TimeBetweenWaves = 3f;
    [SerializeField]
    private UIPlayerHealth[] UIPlayerHealth;

    public static GameManager Instance;

    public CameraFollowComponent cameraFollow;
    public GameObject[] players;

    public GameObject pausePanel;

    private int level;
    private bool startSpawn;
    private float timer;

    private void Awake() {
        if (Instance == null)
            Instance = this;

        timer = 0f;
        startSpawn = false;
        level = 0;

        //Instantiate players
        FindObjectOfType<DeviceManager>().InstantiatePlayers();

        //Enable and setup Health UI
        players = GameObject.FindGameObjectsWithTag("Player");
        
        for(int i = 0; i < players.Length; i++)
        {
            UIPlayerHealth[i].Setup(players[i]);
            UIPlayerHealth[i].transform.parent.gameObject.SetActive(true);
            //Set ActionMap
            players[i].GetComponent<PlayerInput>().SwitchCurrentActionMap("Player Controls");
        }


        //Camera Follow Setup
        cameraFollow.Setup(() => players[0].transform.position);
        
        Debug.Log(players.Length);
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

    public void Reload() {
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
