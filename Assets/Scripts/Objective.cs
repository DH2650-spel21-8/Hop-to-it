using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * Represents a completable objective in the game.
 * Does not need to be placed on any particular object to work.
 *
 * Example usage:
 * Place on an object with a Text component in the UI.
 * Add the interactable that should trigger the completion of the objective to the 'Requirement' field.
 * Add a callback to the 'On Completion' event that changes the Text component's 'text' to "Done!".
 * Enter play mode and have the player interact with the required interactable.
 * Notice the text change to "Done!", when the interaction happens.
 */
public sealed class Objective : MonoBehaviour
{
    public Interactable Requirement;
    public bool Complete;

    [Tooltip("Whether a toggling Interactable will revert this Objective's completion state.")]
    public bool AllowUncompletion;
    
    public UnityEvent<bool> OnCompletion;

    private void Start()
    {
        if (!Requirement)
        {
            Debug.LogWarning("Need an Interactable in order to use an Objective!");
            return;
        }
        
        Requirement.OnInteraction.AddListener(toggle =>
        {
            if (toggle && AllowUncompletion)
            {
                Complete = !Complete;
                OnCompletion.Invoke(Complete);
            }
            else
            {
                if (!Complete)
                {
                    OnCompletion.Invoke(true);
                    Complete = true;
                }
            }
        });
    }
}