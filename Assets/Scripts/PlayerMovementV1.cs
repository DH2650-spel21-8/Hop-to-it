using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementV1 : MonoBehaviour
{
    public float movementSpeed = 10;
    public float turningSpeed = 60;

    private InputAction movementAction; 

    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        var map = new InputActionMap("Simple Player Controller");

        movementAction = map.AddAction("move", binding: "<Gamepad>/leftStick");
        movementAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/s")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/a")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/d")
            .With("Right", "<Keyboard>/rightArrow");
        
        movementAction.Enable();
    }

    void Update()
    {
        var moveDelta = movementAction.ReadValue<Vector2>() * movementSpeed * Time.deltaTime;
        _rb.AddForce(new Vector3(moveDelta.x, 0, moveDelta.y));
    }
}