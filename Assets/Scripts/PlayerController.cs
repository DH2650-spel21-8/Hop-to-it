using System;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public float Speed = 6f;
    public float TurnSmoothTime = 0.1f;
    public string UIReference;

    public CinemachineFreeLook Camera;

    public PlayerInput PlayerInput;
    public Animator CharacterAnimator;

    public Transform Hand;

    private CharacterController _controller;
    private Vector2 _movementInput = Vector2.zero;

    private Camera _playerCam;
    private float _turnSmoothVelocity;

    public Grabbable HeldObject { get; set; }

    public bool IsHolding => HeldObject != null;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        PlayerInput = GetComponent<PlayerInput>();
        PlayerInput.neverAutoSwitchControlSchemes = false;

        _playerCam = PlayerInput.camera;
    }

    private void Update()
    {
        var direction = new Vector3(_movementInput.x, 0, _movementInput.y);


        if (CharacterAnimator)
        {
            CharacterAnimator.SetFloat("Vel X",
                Mathf.Lerp(CharacterAnimator.GetFloat("Vel X"), 0, Time.deltaTime * Speed * 2));
            CharacterAnimator.SetFloat("Vel Y",
                Mathf.Lerp(CharacterAnimator.GetFloat("Vel Y"), 0, Time.deltaTime * Speed * 2));
        }

        // Only move the player if we receive sufficient input
        if (!(direction.sqrMagnitude >= 0.01f)) return;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _playerCam.transform.eulerAngles.y;
        float angle =
            Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, TurnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        _controller.Move((moveDir.normalized * Speed + Physics.gravity) * Time.deltaTime);
        if (CharacterAnimator)
        {
            Vector3 relDir = Quaternion.Euler(0f, -targetAngle, 0f) * moveDir;
            CharacterAnimator.SetFloat("Vel X",
                Mathf.Lerp(CharacterAnimator.GetFloat("Vel X"), relDir.x * Speed, Time.deltaTime * Speed * 2));
            CharacterAnimator.SetFloat("Vel Y",
                Mathf.Lerp(CharacterAnimator.GetFloat("Vel Y"), relDir.z * Speed, Time.deltaTime * Speed * 2));
        }
    }

    private void OnDisable()
    {
        if (CharacterAnimator)
        {
            CharacterAnimator.SetFloat("Vel X", 0);
            CharacterAnimator.SetFloat("Vel Y", 0);
        }
    }

    [UsedImplicitly]
    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }

    [UsedImplicitly]
    public void OnInteract(InputValue value)
    {
    }
}