using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public PinhataAnimHandle playerAnim;
    //public PlayerInputAction inputActions;

    [SerializeField]
    private float Speed = 5f;
    [SerializeField]
    private float TurnSpeed = 5f;
    [SerializeField]
    private Vector2 movementInput;
    [SerializeField]
    private Vector2 rotationInput;

    public bool canMove = true;

    private void OnMove(UnityEngine.InputSystem.InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    private void OnRotate(UnityEngine.InputSystem.InputValue value)
    {
        rotationInput = value.Get<Vector2>();
    }

    private void Update() {
        if (canMove)
            Move();
    }

    private void Move() {
        // Walk
        var movement = new Vector3(movementInput.x, 0, movementInput.y);
        transform.position += movement * Time.deltaTime * Speed;

        // Turn
        var rotation = new Vector3(rotationInput.x, 0, rotationInput.y);
        if(rotation.magnitude > 0.01f) movement = rotation;

        if (movement.magnitude > 0.01f) {
            Quaternion newDirection = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * TurnSpeed);
            playerAnim.Move(movement);
        }else{
            playerAnim.Stop();
        }
    }
}
