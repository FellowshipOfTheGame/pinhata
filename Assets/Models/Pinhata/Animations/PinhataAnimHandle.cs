using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinhataAnimHandle : MonoBehaviour {

    public int dirNumber;
    int lastDir = -1;
    Animator anim;

    // Start is called before the first frame update
    void Awake() {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        if (velocity.magnitude > 0.0f){
            this.transform.position += velocity.normalized * Time.deltaTime;
            Move(velocity.normalized);
        }else{
            Stop();
        }
    }

    public void Move(Vector3 direction){
        anim.SetBool("move", true);

        float angle = Vector3.Angle(direction, Vector3.forward);
        if (direction.x < 0) angle += 180.0f;

        float dir = Mathf.Round(((angle + dirNumber/2) % 360.0f) * dirNumber/ 360.0f);

        if ((int) dir != lastDir){
            anim.SetInteger("dir", (int) dir);
            anim.SetTrigger("refreshMove");
        }
        
        lastDir = (int) dir;
    }

    public void Stop(){
        anim.SetBool("move", false);
        anim.SetInteger("dir", -1);
    }
}
