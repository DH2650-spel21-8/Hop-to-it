using System;
using UnityEngine;

public class Trigger : Interactable
{
    protected override void OnTriggerEnter(Collider other)
    {
        Debug.Log("OI!");
        if (other.TryGetComponent(out PlayerController controller))
        {
            OnInteract(controller);
            OnInteraction.Invoke(Toggle);
        }
    }
}