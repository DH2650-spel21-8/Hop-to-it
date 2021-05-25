using System;
using UnityEngine;

public class Trigger : Interactable
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController controller))
        {
            Application.Quit();
        }
    }
}