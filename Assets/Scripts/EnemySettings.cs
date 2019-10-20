using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySettings : MonoBehaviour {

    [SerializeField] private float speed = 4f;
    public static float Speed => Instance.speed;

    [SerializeField] private float aggroRadius = 1f;
    public static float AggroRadius => Instance.aggroRadius;

    [SerializeField] private float attackRange = 2f;
    public static float AttackRange => Instance.attackRange;

    [SerializeField] private float hp = 100;
    public static float HP => Instance.hp;

    [SerializeField] private float damage = 50;
    public static float Damage => Instance.damage;

    [SerializeField] private int waveSize = 5;
    public static int WaveSize => Instance.waveSize;

    [SerializeField] private float spawnDelay = 0.5f;
    public static float SpawnDelay => Instance.spawnDelay;

    public static EnemySettings Instance;

    private void Awake() {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

}
