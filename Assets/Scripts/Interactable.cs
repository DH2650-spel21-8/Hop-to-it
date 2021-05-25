using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/**
 * The base class for objects that the player can interact with.
 * Based on trigger volumes to determine interaction availability.
 */
public class Interactable : MonoBehaviour
{
    public bool Toggle = false;
    public bool EnableInteraction = true;
    protected virtual void OnInteract(PlayerController player) {}

    private Action<InputAction.CallbackContext> _callback = null;

    public UnityEvent<bool> OnInteraction;

    public UnityEvent OnAttemptedInteraction;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!enabled) return;
        if (other.TryGetComponent(out PlayerController controller))
        {
            // create callback to bind this Interactable to the player's controls as long as the player is within the trigger
            _callback = _ =>
            {
                if (!EnableInteraction)
                {
                    OnAttemptedInteraction.Invoke();
                    return;
                }
                OnInteract(controller);
                OnInteraction.Invoke(Toggle);
            };

            try
            {
                if (controller.PlayerInput.currentActionMap != null)
                    controller.PlayerInput.currentActionMap.FindAction("Interact", true).performed += _callback;
            }
            catch (ArgumentException ae)
            {
                Debug.Log(ae.Message, this);
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (_callback == null) return;
        
        if (other.TryGetComponent(out PlayerController controller))
        {
            // hide prompt and remove action
            
            try
            {
                controller.PlayerInput.currentActionMap.FindAction("Interact", true).performed -= _callback;
            }
            catch (ArgumentException ae)
            {
                Debug.Log(ae.Message, this);
            }
        }
    }
}