using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float Speed = 5f;
    [SerializeField]
    private float TurnSpeed = 5f;

    private void Update() {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        Move(h, v);
    }

    private void Move(float h, float v) {
        // Walk
        var movement = new Vector3(h, 0, v);
        transform.position += movement * Time.deltaTime * Speed;
        // Turn
        if (movement.magnitude > 0) {
            Quaternion newDirection = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * TurnSpeed);
        }
    }
}
