using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    private NavMeshAgent navMesh;
    private GameObject[] players;
    private MariachiAnimHandle anim;
    public Collider mainCollider;
    
    bool canMove = false;

    private void Start() {
        navMesh = GetComponent<NavMeshAgent>();
        players = GameManager.Instance.players;//GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(players.Length);
        //Debug.Log(GameManager.Instance.players.Length);

        anim = GetComponentInChildren<MariachiAnimHandle>();
        anim.Spawn();
        //Invoke("Chase", 4.5f);
        mainCollider.enabled = false;
        navMesh.enabled=false;
    }

    public void Activate(){
        mainCollider.enabled = true;
        navMesh.enabled = true;
        this.GetComponent<EnemyHealth>().invincible = false;
        Chase();
    }

    void Chase(){
        canMove = true;
        //navMesh.Resume();
        navMesh.isStopped = false;
    }

    public void PauseMovement(float time){
        canMove = false;
        Invoke("Chase", time);
        //navMesh.Stop();
        navMesh.isStopped = true;
    }

    private void Update() {
        if(canMove)
            Move();
    }

    private void Move() {
        if (players != null) {
            int closestPlayer = 0;
            float distance = Mathf.Infinity;//Vector3.Distance(this.transform.position, players[0].transform.position);
            for(int i = 1; i < players.Length; i++)
            {
                if (players[i] == null) continue;
                float current_distance = Vector3.Distance(this.transform.position, players[i].transform.position);
                if (current_distance < distance)
                {
                    closestPlayer = i;
                    distance = current_distance;
                }
            }
            navMesh.SetDestination(players[closestPlayer].transform.position);
            anim.Move(navMesh.velocity);
        }
        else {
            navMesh.enabled = false;
            anim.Stop();
        }
    }
}
