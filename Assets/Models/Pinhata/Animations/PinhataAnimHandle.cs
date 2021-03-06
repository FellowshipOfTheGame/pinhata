﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinhataAnimHandle : MonoBehaviour {

    public int dirNumber;
    int lastDir = -1;
    Animator anim;
    public SkinnedMeshRenderer mesh;
    public Material[] blueMats, orangeMats, purpleMats, greenMats;

    // Start is called before the first frame update
    void Awake() {
        anim = this.GetComponent<Animator>();
    }

    public void changeMat(int index){
        switch (index){
            case 0:
                mesh.materials = blueMats;
                break;
            case 1:
                mesh.materials = orangeMats;
                break;
            case 2:
                mesh.materials = purpleMats;
                break;
            case 3:
                mesh.materials = greenMats;
                break;
        }
        //Debug.Log("MUDOU!");
    }

    // Update is called once per frame
    void Update() {
    
    }

    public void Move(Vector3 direction){
        anim.SetBool("move", true);

        float angle = Vector3.Angle(direction, Vector3.forward);
        if (direction.x < 0) angle += 180.0f;

        //float dir = Mathf.Round(((angle + dirNumber/2) % 360.0f) * dirNumber/ 360.0f);
        float dir = 0.0f;

        //if ((int) dir != lastDir){
            anim.SetInteger("dir", (int) dir);
            anim.SetTrigger("refreshMove");
        //}
        
        lastDir = (int) dir;
    }

    public void Stop(){
        anim.SetBool("move", false);
        //anim.ResetTrigger("refreshMove");
        anim.SetInteger("dir", -1);
    }

    public void Shoot(){
        anim.SetTrigger("shoot");
    }

    public void Die(){
        anim.SetTrigger("die");
    }

    public void TakeHit(){
        anim.SetTrigger("hit");
    }
}
