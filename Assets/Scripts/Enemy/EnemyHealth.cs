﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField]
    private int MaxHealth = 10;
    [SerializeField]
    private GameObject ItemPrefab = null;
    [SerializeField]
    [Range(0, 1)]
    private float DropItemRate = 1;

    private int health;

    private void Awake() {
        health = MaxHealth;
    }

    public bool Alive() {
        return health > 0;
    }

    private void Die() {
        gameObject.SetActive(false);
    }

    private void Drop() {
        float random = Random.Range(0, 100) / 100f;
        if (random <= DropItemRate) {
            var position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(ItemPrefab, position, ItemPrefab.transform.rotation);
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;

        if (!Alive()) {
            Drop();
            Die();
        }
    }
}