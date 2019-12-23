using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public PinhataAnimHandle playerAnim;
    public PlayerInputAction inputActions;

    [SerializeField]
    private float Speed = 5f;
    [SerializeField]
    private float TurnSpeed = 5f;
    [SerializeField]
    private Vector2 movementInput;
    [SerializeField]
    private Vector2 rotationInput;

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        inputActions.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerControls.Rotate.performed += ctx => rotationInput = ctx.ReadValue<Vector2>();
    }

    private void Update() {
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
            movement = Vector3.zero;
            rotation = Vector3.zero;
            playerAnim.Stop();
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
