using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerHandler : MonoBehaviour
{
    public enum Mode
    {
        Multiplayer = 1,
        Singleplayer = 0
    }

    public UIDocument GameUI;

    // Represents every player in the game
    public List<PlayerController> Controllers = new List<PlayerController>();
    public InputAction SwapAction;
    public InputAction SwitchAction;

    private readonly List<PlayerData> _playerData = new List<PlayerData>();

    private int _active = -1;

    private PlayerInputManager _inputManager;

    private Mode _mode = Mode.Singleplayer;

    private void Start()
    {
        _inputManager = GetComponent<PlayerInputManager>();
        
        SetupActions();

        foreach (PlayerController controller in Controllers)
        {
            var data = new PlayerData
            {
                Input = controller.PlayerInput,
                Controller = controller,
                Camera = controller.PlayerInput.camera,
            };
            data.Listener = data.Camera.GetComponent<AudioListener>();
            
            if (GameUI)
            {
                var playerUI = GameUI.rootVisualElement.Q<VisualElement>(controller.UIReference);
                if (playerUI != null)
                {
                    data.UI = playerUI;
                    playerUI.style.display = DisplayStyle.None;
                }
            }
            _playerData.Add(data);
        }
        
        DisableAllPlayers();
        SetActivePlayer(0);
        
        
    }

    private void SetupActions()
    {
        SwapAction.performed += context => { CyclePlayers(); };
        SwitchAction.performed += context => { SwitchMode(); };

        SwapAction.Enable();
        if (Gamepad.current != null)
        {
            SwitchAction.Enable();
        }
    }

    private void SwitchMode()
    {
        _mode = 1 - _mode;
        switch (_mode)
        {
            case Mode.Multiplayer:
                // In multiplayer mode, all controllers are active, and split-screen is enabled
                // Cycling players is disabled in this mode

                foreach (PlayerData data in _playerData)
                {
                    data.Controller.enabled = true;
                    data.Camera.enabled = true;
                    data.Input.ActivateInput();
                    
                    data.UI.style.display = DisplayStyle.Flex;
                }

                _playerData[1].Input.SwitchCurrentControlScheme(Keyboard.current, Mouse.current);
                if (Gamepad.current != null)
                {
                    _playerData[0].Input.SwitchCurrentControlScheme(Gamepad.current);
                }
                
                _inputManager.splitScreen = true;
                
                SwapAction.Disable();
                break;
            case Mode.Singleplayer:
                // In singleplayer mode only one controller is active at a time (set with SetActivePlayer(playerIndex)), and split-screen is disabled
                _inputManager.splitScreen = false;
                
                DisableAllPlayers();

                SetActivePlayer(_active);
                
                SwapAction.Enable();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void DisableAllPlayers()
    {
        foreach (PlayerData data in _playerData)
        {
            data.Camera.enabled = false;
            data.Listener.enabled = false;
            data.Controller.enabled = false;
            data.Input.DeactivateInput();

            data.UI.style.display = DisplayStyle.None;
        }
    }

    private void SetActivePlayer(int index)
    {
        if (index >= Controllers.Count) return;

        if (_active == -1)
        {
            PlayerData data = _playerData[index];
            
            data.Camera.enabled = true;
            data.Listener.enabled = true;

            data.Controller.enabled = true;
            data.Input.ActivateInput();

            data.UI.style.display = DisplayStyle.Flex;
        }
        else
        {
            int prev = _active;

            PlayerData prevData = _playerData[prev];
            PlayerData data = _playerData[index];

            prevData.UI.style.display = DisplayStyle.None;
            data.UI.style.display = DisplayStyle.Flex;

            prevData.Camera.enabled = false;
            data.Camera.enabled = true;

            prevData.Controller.enabled = false;
            data.Controller.enabled = true;
            
            prevData.Listener.enabled = false;
            data.Listener.enabled = true;
            
            prevData.Input.DeactivateInput();
            data.Input.ActivateInput();
            data.Input.SwitchCurrentControlScheme(Keyboard.current, Mouse.current);
        }

        _active = index;
    }

    private void CyclePlayers()
    {
        int current = _active;
        current++;
        if (current == Controllers.Count) current = 0;

        SetActivePlayer(current);
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        _mode = Mode.Multiplayer;
    }

    public void OnPlayerLeft(PlayerInput player)
    {
        _mode = Mode.Singleplayer;
    }

    private struct PlayerData
    {
        public PlayerInput Input;
        public PlayerController Controller;
        public Camera Camera;
        public VisualElement UI;
        public AudioListener Listener;
    }
}