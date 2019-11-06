using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField]
    private int Damage = 2;
    [SerializeField]
    private float AttackDelay = 2f;

    private float timer;
    private EnemyHealth health;

    private void Awake() {
        timer = 0f;
        health = GetComponent<EnemyHealth>();
    }

    private void Update() {
        timer += Time.deltaTime;

        if (CanAttack()) {
            timer = 0f;
            Attack();
        }
    }

    private bool CanAttack() {
        return (health.Alive() && timer >= AttackDelay);
    }

    private void Attack() { 
        Debug.DrawRay(transform.position, transform.forward * 5, Color.yellow, 2f);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        // Hit player
        if (Physics.Raycast(ray, out hitInfo, 1.5f)) {
            var player = hitInfo.collider.gameObject.GetComponent<PlayerHealth>();
            if (player != null) {
                player.TakeDamage(Damage);
            }
        }
    }
}