using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerObjectives : MonoBehaviour
{
    public PlayerController Controller;
    
    [Space(10)]
    public UIDocument UI;
    [Tooltip("Name of the UI element for representing an objective, relative to this Player's UI. Must contain elements of name \"title\" and \"description\" if these should be shown in the UI.")]
    public string ObjectiveUIReference;
        
    [Space(10)]
    public List<Objective> Objectives;
    
    
    private VisualElement _playerUI;
    private VisualElement _objectiveElement;
    private TextElement _titleElement;
    private TextElement _descElement;
    private void Start()
    {
        string uiRef = Controller.UIReference;
        _playerUI = UI.rootVisualElement.Q<VisualElement>(uiRef);
        _objectiveElement = _playerUI.Q<VisualElement>(ObjectiveUIReference);
        
        if (_objectiveElement != null)
        {
            // May or may not exist
            _titleElement = _objectiveElement.Q<TextElement>("title");
            _descElement = _objectiveElement.Q<TextElement>("description");
        }
        else
        {
            Debug.LogWarning("Invalid UI reference for objective element. Player objective will not be shown.");
            return;
        }
        
        if (Objectives.Count != 0)
        {
            SetObjective(0);
        }
    }

    private void SetObjective(int idx)
    {
        if (idx >= Objectives.Count) return;
        Objective objective = Objectives[idx];
        int next = idx + 1;
        if (objective.Complete)
        {
            SetObjective(next);
            return;
        }

        objective.OnCompletion.AddListener(complete =>
        {
            SetObjective(next);
        });

        if (_titleElement != null)
        {
            _titleElement.text = objective.Title;
        }

        if (_descElement != null)
        {
            _descElement.text = objective.Description;
        }
    }
}