using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private int MaxHealth = 20;

    private int health;

    private PinhataAnimHandle anim;

    private bool invincible;

    private void Awake() {
        health = MaxHealth;
        anim = GetComponent<PlayerMovement>().playerAnim;
        invincible = false;
    }

    public bool Alive() {
        return health > 0;
    }

    private void Die() {
        invincible = true;
        Debug.Log("Player Dead");
        anim.Die();
        GameManager.Instance.Invoke("Reload", 3.0f);

    }

    public void TakeDamage(int damage) {
        if(!invincible){
        health -= damage;

        if (!Alive())
            Die();

        }
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
