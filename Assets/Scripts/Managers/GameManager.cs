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

    //public CameraFollowComponent cameraFollow;
    public GameObject[] players;
    public GameObject[] cameras;

    public GameObject camera_prefab;
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
        cameras = new GameObject[players.Length];
        //Set alive count
        players_alive_count = players.Length;

        for (int i = 0; i < players.Length; i++) {
            GameObject p = players[i];
            //Set health UI
            UIPlayerHealth[i].Setup(p);
            UIPlayerHealth[i].transform.parent.gameObject.SetActive(true);
            //Setup Camera Follow
            GameObject cam = Instantiate(camera_prefab);
            cameras[i] = cam;
            cam.GetComponent<CameraFollowComponent>().Setup(() => p.transform.position);
            //Set ActionMap
            p.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player Controls");
            p.GetComponent<PlayerInput>().camera = cam.GetComponent<Camera>();
        }
        if (SoundManager.Instance != null) SoundManager.Instance.PlayMusicLoop(0);
        ReajustCameras();
        //Debug.Log(players.Length);
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
        {
            Reload();
            return;
        }
        //Destruir camera associada ao player morto
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i] == null && cameras[i] != null)
            {
                Destroy(cameras[i]);
                UIPlayerHealth[i].transform.parent.gameObject.SetActive(false);
            }
        }
        ReajustCameras();
    }

    void ReajustCameras()
    {
        int c = 0;
        switch (players_alive_count)
        {
            case 1:
                for (int i = 0; i < cameras.Length; i++)
                {
                    if(players[i] != null && cameras[i] != null)
                    {
                        cameras[i].GetComponent<Camera>().rect = new Rect(0f, 0f, 1f, 1f);
                    }
                }
                break;
            case 2:
                c = 0;
                for (int i = 0; i < cameras.Length; i++)
                {
                    if (players[i] != null && cameras[i] != null)
                    {
                        if (c == 0)
                            cameras[i].GetComponent<Camera>().rect = new Rect(0f, 0f, .5f, 1f);
                        else
                            cameras[i].GetComponent<Camera>().rect = new Rect(.5f, 0f, .5f, 1f);
                        c++;
                    }
                }
                break;
            case 3:
                c = 0;
                for (int i = 0; i < cameras.Length; i++)
                {
                    if (players[i] != null && cameras[i] != null)
                    {
                        if (c == 0)
                            cameras[i].GetComponent<Camera>().rect = new Rect(0f, 0.5f, .5f, .5f);
                        else if(c == 1)
                            cameras[i].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, .5f, .5f);
                        else if(c == 2)
                            cameras[i].GetComponent<Camera>().rect = new Rect(0f, 0f, 1f, .5f);
                        c++;
                    }
                }
                break;
            case 4:
                cameras[0].GetComponent<Camera>().rect = new Rect(0.0f, 0.5f, .5f, .5f);
                cameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, .5f, .5f);
                cameras[2].GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, .5f, .5f);
                cameras[3].GetComponent<Camera>().rect = new Rect(0.5f, 0.0f, .5f, .5f);
                break;
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