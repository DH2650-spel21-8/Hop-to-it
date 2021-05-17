using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Objective : MonoBehaviour
{
    public List<Interactable> Requirements;
    public bool Complete;
    
    public UnityEvent OnCompletion;

    private void Start()
    {
        foreach (Interactable obj in Requirements)
        {
            
        }
    }
}

public struct ObjectiveInfo
{
    
}