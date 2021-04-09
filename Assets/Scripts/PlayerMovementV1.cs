using System;
using UnityEngine;

public class PlayerMovementV1 : MonoBehaviour
{
    public float movementSpeed = 10;
    public float turningSpeed = 60;

    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        _rb.AddForce(new Vector3(horizontal, 0, 0));
        //transform.Translate(horizontal, 0, 0);

        float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        
        _rb.AddForce(new Vector3(0, 0, vertical));
        //transform.Translate(0, 0, vertical);
    }

    private void OnCollisionEnter(Collision other)
    {
        var hits = other.contacts;
    }
}