using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Grabbable : Activateable
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

    public override void OnActivate(PlayerController player)
    {
        if (_hand)
        {
            _rb.freezeRotation = false;
            _rb.useGravity = true;
            _hand = null;
        } else
        {
            _hand = player.Hand;
            _rb.useGravity = false;
            _rb.freezeRotation = true;
        }
    }

    private void Update()
    {
        if (_hand)
        {
            _rb.position = _hand.position;
        }
    }
}