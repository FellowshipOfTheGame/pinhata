using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField]
    private int Health = 2;
    [SerializeField]
    private float AvailableTime = 3f;

    private float awokeTime;

    private void Awake() {
        awokeTime = Time.deltaTime;
    }

    private void Update() {
        DestroyItem();
    }

    private void DestroyItem() {
        var time = Time.deltaTime - awokeTime;
        if (time >= AvailableTime) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        var player = other.gameObject.GetComponent<PlayerHealth>();
        if (player != null) {
            player.Cure(Health);
            Destroy(gameObject);
        }
    }
}