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
    [SerializeField]
    private LayerMask layerMask;

    private float timer;
    private PlayerHealth health;
    private PinhataAnimHandle anim;

    public float bulletSpeed;
    public GameObject bullet;

    private void Awake() {
        timer = 0f;
        health = GetComponent<PlayerHealth>();
        anim = GetComponent<PlayerMovement>().playerAnim;
    }

    private void Update() {
        timer += Time.deltaTime;
    }

    private void OnShoot()
    {
        if (CanShoot())
        {
            timer = 0f;
            // Bullets are player's health
            health.TakeDamage(1, false);
            FireGun();
            //SoundManager.Instance.PlayClip(Sounds.PlayerAttack_1, health.audioSource);
        }
    }

    private void FireGun() {
        Debug.DrawRay(FirePoint.position, FirePoint.forward * 100, Color.red, 2f);

        anim.Shoot();
        Ray ray = new Ray(FirePoint.position, FirePoint.forward);
        RaycastHit hitInfo;

        Projectile b = Instantiate(bullet).GetComponent<Projectile>();
        b.transform.position = FirePoint.position;
        b.generator = this.transform;
        b.speed = FirePoint.forward * bulletSpeed * Time.deltaTime;

        // Hit enemy
        if(Physics.Raycast(ray, out hitInfo, 100, layerMask)) {
            var enemy = hitInfo.collider.gameObject.GetComponent<EnemyHealth>();
            
            b.target = hitInfo.point;
            if (enemy != null) {
                
                enemy.TakeDamage(Damage);
            }
        }
    }

    private bool CanShoot() {
        return (health.Alive() && timer >= ShootDelay);
    }
}
