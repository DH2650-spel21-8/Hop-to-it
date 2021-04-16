using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10.0f;
    private Vector2 movementInput = Vector2.zero;

    private Rigidbody _rb;


    public PlayerInput playerInput;

    public Transform Hand;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        playerInput = gameObject.GetComponent<PlayerInput>();
        playerInput.neverAutoSwitchControlSchemes = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    void Update()
    {

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        _rb.AddForce(move * Time.deltaTime * movementSpeed);
        /*
         * This turns the player and camera facing the direction they're moving in
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        */
    }

}