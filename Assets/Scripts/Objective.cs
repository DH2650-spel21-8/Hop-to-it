using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Objective : MonoBehaviour
{
    public Interactable Requirement;
    public bool Complete;
    
    public UnityEvent<bool> OnCompletion;

    private void Start()
    {
        Requirement.OnInteraction.AddListener(toggle =>
        {
            if (toggle)
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