using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private float TimeBetweenWaves = 3f;

    public static GameManager Instance;

    public CameraFollowComponent cameraFollow;
    private Transform playerTransform;

    private int level;
    private bool startSpawn;
    private float timer;

    private void Awake() {
        if (Instance == null)
            Instance = this;

        timer = 0f;
        startSpawn = false;
        level = 0;

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //Camera Follow Setup
        cameraFollow.Setup(() => playerTransform.position);
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
        SceneManager.LoadScene(0);
    }
}
