using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance : MonoBehaviour
{
    public Animator dancer;
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public void StartDancing(){
        Debug.Log("DANCE!");
        dancer.SetTrigger("dance");
        source.Play();
    }
}
