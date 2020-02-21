using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour{

    public Transform generator;
    public Vector3 speed;

    public Vector3 target = Vector3.zero;
    Vector3 start;
    public TrailRenderer trail;
    public ParticleSystem particle;

    bool exploding = false;
    void Awake() {
        start = this.transform.position;
    }

    void FixedUpdate(){
        if (!exploding){

            if(target != Vector3.zero && (this.transform.position - target).magnitude < speed.magnitude){

            //if (target != Vector3.zero && (this.transform.position - target).magnitude < 0.5){
            this.transform.position = target;
            //generator = this.transform;
            // target = Vector3.zero;
            // this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Explode();
            }
            this.transform.position += speed;
        }
        else{
            trail.transform.position += speed;
        }
    }

    void OnTriggerEnter(Collider col){
        //if (generator == null || generator != col.transform){
        //    if(target != Vector3.zero)
        //    this.transform.position = target;
        //    Explode();
        //}
    }

    public void Explode(){
        exploding = true;
        trail.emitting = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        if(particle != null){
            particle.Play();
            Invoke("Vanish", 1f);
        }else{
            Destroy(this.gameObject);
        }
    }

    void Vanish() {
        Destroy(this.gameObject);
    }
}
