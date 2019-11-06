using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    private NavMeshAgent navMesh;
    private GameObject player;

    private void Awake() {
        navMesh = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        Move();
    }

    private void Move() {
        if (player != null) {
            navMesh.SetDestination(player.transform.position);
        }
        else {
            navMesh.enabled = false;
        }
    }
}
