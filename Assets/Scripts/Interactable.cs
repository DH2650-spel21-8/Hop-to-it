using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public bool Toggle;
    protected virtual void OnInteract(PlayerController player) {}

    private Action<InputAction.CallbackContext> _callback;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController controller))
        {
            // show a prompt near the player or near the switch for activating
            
            // create callback to bind the Activateable to the player's controls
            _callback = _ =>
            {
                OnInteract(controller);
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