using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/**
 * The base class for objects that the player can interact with.
 * Based on trigger volumes to determine interaction availability.
 */
public class Interactable : MonoBehaviour
{
    public bool Toggle;
    protected virtual void OnInteract(PlayerController player) {}

    private Action<InputAction.CallbackContext> _callback;

    public UnityEvent<bool> OnInteraction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController controller))
        {
            // show a prompt near the player or near the switch for activating
            
            // create callback to bind the Activateable to the player's controls
            _callback = _ =>
            {
                OnInteract(controller);
                OnInteraction.Invoke(Toggle);
            };

            try
            {
                controller.PlayerInput.currentActionMap.FindAction("Interact", true).performed += _callback;
            }
            catch (ArgumentException ae)
            {
                Debug.Log(ae.Message, this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
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