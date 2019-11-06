using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    [SerializeField]
    private int Damage = 2;
    [SerializeField]
    private float ShootDelay = 2f;
    [SerializeField]
    private Transform FirePoint = null;

    private float timer;
    private PlayerHealth health;

    private void Awake() {
        timer = 0f;
        health = GetComponent<PlayerHealth>();
    }

    private void Update() {
        timer += Time.deltaTime;

        if(CanShoot()) {
            if(Input.GetButtonDown("Fire1")) {
                timer = 0f;
                // Bullets are player's health
                health.TakeDamage(1);
                FireGun();
            }
        }
    }

    private void FireGun() {
        Debug.DrawRay(FirePoint.position, FirePoint.forward * 100, Color.red, 2f);

        Ray ray = new Ray(FirePoint.position, FirePoint.forward);
        RaycastHit hitInfo;

        // Hit enemy
        if(Physics.Raycast(ray, out hitInfo, 100)) {
            var enemy = hitInfo.collider.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null) {
                enemy.TakeDamage(Damage);
            }
        }
    }

    private bool CanShoot() {
        return (health.Alive() && timer >= ShootDelay);
    }
}
