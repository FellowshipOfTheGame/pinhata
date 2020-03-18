using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField]
    private int Damage = 2;
    [SerializeField]
    private float AttackDelay = 2f, AttackReload = 2f;

    private float timer;
    private EnemyHealth health;
    private EnemyMovement movement;
    private MariachiAnimHandle anim;
    private PlayerHealth player = null;

    private void Awake() {
        timer = 0f;
        health = GetComponent<EnemyHealth>();
        anim = GetComponentInChildren<MariachiAnimHandle>();
        movement = GetComponent<EnemyMovement>();
    }

    private void Update() {
        timer += Time.deltaTime;

        if (CanAttack()) {
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
        //if (Physics.Raycast(ray, out hitInfo, 1.5f)) {
            //var player = hitInfo.collider.gameObject.GetComponent<PlayerHealth>();
            if (player != null && player.Alive()) {
                timer = 0f;
                //movement.PauseMovement(0.84f);
                //anim.Attack();
                //player.TakeDamage(Damage);
                //Invoke("DealDamage", 0.25f);
                Invoke("Punch", 0.25f);
            }
       // }
    }

    void Punch(){
        if (player != null) {
                timer = 0f;
                movement.PauseMovement(0.84f);
                anim.Attack();
                //SoundManager.Instance.PlayClip(Sounds.EnemyAttack, health.audioSource);
                //player.TakeDamage(Damage);
                Invoke("DealDamage", 0.25f);
            }
    }

    void DealDamage(){
        if (player != null && player.Alive()) {
            player.TakeDamage(Damage, true);
            anim.LandAttack();
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player"){
            player = col.gameObject.GetComponent<PlayerHealth>();
        }
    }

     void OnTriggerExit(Collider col){
        if(col.gameObject.tag == "Player"){
            player = null;
        }
    }
}