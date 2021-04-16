using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Grabbable : MonoBehaviour
{

    private SphereCollider _col;
    public float Radius;
    private Rigidbody _rb;

    private Transform _hand;
    private void Start()
    {
        _col = GetComponent<SphereCollider>();
        _rb = GetComponent<Rigidbody>();
        _col.radius = Radius;
    }

    private void Update()
    {
        if (_hand)
        {
            _rb.position = _hand.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController controller))
        {
            _hand = controller.Hand;
            _rb.useGravity = false;
        }
    }
}