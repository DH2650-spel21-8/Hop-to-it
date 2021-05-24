using System;
using System.Collections;
using UnityEngine;

public class SlidingDoor : Interactable
{
    public GameObject DoorObject;
    public Transform Target;

    private Vector3 _start;

    public float OpenSpeed;
    private bool _open = false;
    private Vector3 _openDist;

    private void Start()
    {
        _start = DoorObject.transform.position;
        _openDist = (Target.position - _start);
    }

    protected override void OnInteract(PlayerController player)
    {
        _open = !_open;
    }

    private void Update()
    {
        if (_open)
        {
            DoorObject.transform.position = Vector3.Lerp(DoorObject.transform.position, _start + _openDist, Time.deltaTime * OpenSpeed);
        }
        else
        {
            DoorObject.transform.position = Vector3.Lerp(DoorObject.transform.position, _start, Time.deltaTime * OpenSpeed);
        }
    }
}