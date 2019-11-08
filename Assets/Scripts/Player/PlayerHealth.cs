using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private int MaxHealth = 20;

    private int health;

    private PinhataAnimHandle anim;

    private void Awake() {
        health = MaxHealth;
        anim = GetComponent<PlayerMovement>().playerAnim;
    }

    public bool Alive() {
        return health > 0;
    }

    private void Die() {
        Debug.Log("Player Dead");
        anim.Die();
        //GameManager.Instance.Reload();

    }

    public void TakeDamage(int damage) {
        health -= damage;

        if (!Alive())
            Die();
    }

    public void Cure(int cure) {
        if (health + cure >= MaxHealth) {
            health = MaxHealth;
        }
        else {
            health += cure;
        }
    }
}
