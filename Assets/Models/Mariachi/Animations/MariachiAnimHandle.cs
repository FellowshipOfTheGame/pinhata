using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariachiAnimHandle : MonoBehaviour {

    public Animator anim;

    void Awake() {

    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Move(Vector3 velocity) {
        if(velocity.magnitude > 0.35f){
            anim.SetBool("moving", true);
        }else{
            anim.SetBool("moving", false);
        }
        
    }

    public void Stop() {
        anim.SetBool("moving", false);
    }

    public void Die(){
        this.transform.SetParent(null);
        anim.SetTrigger("die");
        Invoke("Vanish", 1.7f);
    }

    void Vanish(){
        Destroy(this.gameObject);
    }

    public void Spawn(){
        anim.SetTrigger("spawn");
    }

    public void Attack(){
        anim.SetTrigger("hit");
    }

    public void TakeDamage() {
        anim.SetTrigger("damage");
    }
}
