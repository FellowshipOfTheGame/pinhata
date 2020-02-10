using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private GameObject EnemyPrefab = null;
    [SerializeField]
    private SpawnPoint[] SpawnPoints = null;
    [SerializeField]
    private int MinWaveSize = 5;
    [SerializeField]
    private float SpawnDelay = 0.7f;
    [SerializeField]
    private bool CanSpawn = false;

    public static EnemyManager Instance;

    private int spawned;
    private int enemiesAlive;
    private int waveSize;
    private float timer;
    
    private void Awake() {
        if (Instance == null)
            Instance = this;

        spawned = 0;
        enemiesAlive = 0;
        waveSize = MinWaveSize;
        timer = 0f;
    }

    private void Update() {
        if (CanSpawn) {
            timer += Time.deltaTime;

            if (timer >= SpawnDelay) {
                Spawn();
            }

            if (spawned == waveSize) {
                CanSpawn = false;
                timer = 0f;
                spawned = 0;
            }
        }
    }

    private void Spawn() {
        int spawnPointIndex = Random.Range(0, SpawnPoints.Length);
        if (SpawnPoints[spawnPointIndex].occuped){
            int count = 1;
            while(SpawnPoints[spawnPointIndex].occuped && count < SpawnPoints.Length){
                spawnPointIndex++;
                if(spawnPointIndex >= SpawnPoints.Length) spawnPointIndex = 0;
                count++;
            }

            if(!SpawnPoints[spawnPointIndex].occuped){
                SpawnPoints[spawnPointIndex].Spawn(EnemyPrefab);
                spawned++;
                timer = 0f;
            }
        }else{
            SpawnPoints[spawnPointIndex].Spawn(EnemyPrefab);
            spawned++;
            timer = 0f;
        }
    }

    public bool AreEnemiesAlive() {
        return enemiesAlive > 0;
    }

    public void EnemyDied() {
        enemiesAlive--;
    }

    public void SetSpawn(int level) {
        waveSize = MinWaveSize * level;
        enemiesAlive = waveSize;
        CanSpawn = true;
    }
}
