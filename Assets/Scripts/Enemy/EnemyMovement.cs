using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    private NavMeshAgent navMesh;
    private GameObject player;
    private MariachiAnimHandle anim;
    
    bool canMove = false;

    private void Awake() {
        navMesh = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<MariachiAnimHandle>();
        anim.Spawn();
        Invoke("Chase", 4.5f);
    }

    void Chase(){
        canMove = true;
        navMesh.Resume();
    }

    public void PauseMovement(float time){
        canMove = false;
        Invoke("Chase", time);
        navMesh.Stop();
    }

    private void Update() {
        if(canMove)
            Move();
    }

    private void Move() {
        if (player != null) {
            navMesh.SetDestination(player.transform.position);
            anim.Move(navMesh.velocity);
        }
        else {
            navMesh.enabled = false;
            anim.Stop();
        }
    }
}
