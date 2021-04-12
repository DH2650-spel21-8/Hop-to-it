using System;
using UnityEngine;

public class PlayerMovementV1 : MonoBehaviour
{
    public float movementSpeed = 10;
    public float turningSpeed = 60;
    private bool player2;
    private bool multiplayer; 
    
    //{ get {}; set; }; //C# properties

    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        player2 = false;
        multiplayer = false;
    }

    void Update()
    {
        float horizontal = 0.0f;
        float vertical = 0.0f;

        //Debug.Log("Multiplayer: " + multiplayer + " Player 2: " + player2);

        // Single player mode
        if(!multiplayer && !player2)
        {
           // Debug.Log("Single player mode");
            horizontal = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
            vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        }

        // Two player mode where this player is player1
        else if(multiplayer && !player2)
        {
            Debug.Log("Two player mode. Player1");
            horizontal = Input.GetAxis("WASDHorizontal") * movementSpeed * Time.deltaTime;
            vertical = Input.GetAxis("WASDVertical") * movementSpeed * Time.deltaTime;
        }

        // Two player mode where this player is player2
        else if(multiplayer && player2)
        {
            Debug.Log("Two player mode. Player2");
            horizontal = Input.GetAxis("ArrowHorizontal") * movementSpeed * Time.deltaTime;
            vertical = Input.GetAxis("ArrowVertical") * movementSpeed * Time.deltaTime;
        }

        _rb.AddForce(new Vector3(horizontal, 0, 0));
        //transform.Translate(horizontal, 0, 0);

        _rb.AddForce(new Vector3(0, 0, vertical));
        //transform.Translate(0, 0, vertical);
    }

    private void OnCollisionEnter(Collision other)
    {
        var hits = other.contacts;
    }

    public void EnableMultiplayer()
    {
        multiplayer = true;
    }

    public void DisableMultiplayer()
    {
        multiplayer = false;

    }

    public void SetPlayerTwo(bool p2)
    {
        player2 = p2;
    }
}