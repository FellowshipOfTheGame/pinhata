using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private int MaxHealth = 50;

    private int health;

    private void Awake() {
        health = MaxHealth;
    }

    private bool Alive() {
        return health > 0;
    }

    private void Die() {

    }

    public void TakeDamage(int damage) {
        health -= damage;

        if (!Alive())
            Die();
    }
}
