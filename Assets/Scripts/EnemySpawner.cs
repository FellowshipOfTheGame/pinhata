using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    public GameObject enemyPrefab;

    private int spawned = 0;

    private float nextSpawnTime;

    private void Update() {
        if (ShouldSpawn())
            Spawn();
    }

    private bool ShouldSpawn() {
        return (Time.time >= nextSpawnTime && spawned < EnemySettings.WaveSize);
    }

    private void Spawn() {
        nextSpawnTime = Time.time + EnemySettings.SpawnDelay;
        spawned++;

        /* 
         * Podemos acrescentar uma aleatoriedade na escolha dos inimigos
         * que serao instanciados, caso de tempo de fazer mais de um inimgo.
         * Ou fazer spawner diferentes para inimgos diferentes.
         */
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }
}
