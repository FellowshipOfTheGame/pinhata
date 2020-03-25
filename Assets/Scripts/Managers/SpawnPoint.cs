using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public static float spawnTime = 4.5f;
    float timer;
    public Transform point;
    public bool occuped = false;

    EnemyMovement enemy = null;
    void Update(){
        if (occuped){
            timer += Time.deltaTime;
            if (timer >= spawnTime){
                enemy.Activate();
                enemy = null;
                occuped = false;
            }
        }
    }

    public void Spawn(GameObject EnemyPrefab){
        enemy = Instantiate(EnemyPrefab, point.position, point.rotation).GetComponent<EnemyMovement>();
        occuped = true;
        timer = 0f;
    }
}
