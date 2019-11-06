using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private GameObject EnemyPrefab;
    [SerializeField]
    private Transform[] SpawnPoints;

    public int WaveSize = 5;
    public float SpawnDelay = 0.7f;
    public bool CanSpawn = false;

    private int spawned;
    private float timer;

    private void Awake() {
        spawned = 0;
        timer = 0f;
    }

    private void Update() {
        timer += Time.deltaTime;

        if (ShouldSpawn()) {
            Spawn();
        }
        else if(spawned == WaveSize) {
            CanSpawn = false;
            spawned = 0;
            timer = 0f;
        }
    }

    private bool ShouldSpawn() {
        return (CanSpawn && timer >= SpawnDelay && spawned < WaveSize);
    }

    private void Spawn() {
        int spawnPointIndex = Random.Range(0, SpawnPoints.Length);
        Instantiate(EnemyPrefab, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
        spawned++;
        timer = 0f;
    }
}
