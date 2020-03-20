using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public delegate void OnHealthChange(int amount);
    public OnHealthChange onHealthChange;

    [SerializeField]
    public int MaxHealth = 20;

    public AudioSource audioSource;

    private int health;

    private PinhataAnimHandle anim;

    private bool invincible;

    private void Awake() {
        health = MaxHealth;
        onHealthChange?.Invoke(health);
        anim = GetComponent<PlayerMovement>().playerAnim;
        audioSource = GetComponent<AudioSource>();
        invincible = false;
    }

    public bool Alive() {
        return health > 0;
    }

    private void Die() {
        invincible = true;
        this.GetComponent<PlayerMovement>().canMove = false;
        Debug.Log("Player Dead");
        anim.Die();
        GameManager.Instance.Invoke("Reload", 3.0f);

    }

    public void TakeDamage(int damage, bool enemy) {
        if(!invincible){
            health -= damage;
            if (enemy) {
                anim.TakeHit();
                //SoundManager.Instance.PlayClip(Sounds.PlayerTakeDamage, audioSource);
            }
            if (!Alive())
                Die();
        }
        onHealthChange?.Invoke(health);
    }

    public void Cure(int cure) {
        if (health + cure >= MaxHealth) {
            health = MaxHealth;
        }
        else {
            health += cure;
        }
        onHealthChange?.Invoke(health);
    }
}
