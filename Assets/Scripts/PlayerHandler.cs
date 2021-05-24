using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{

    public enum Mode
    {
        Multiplayer = 1,
        Singleplayer = 0
    }

    // Assigned from Controllers
    private readonly List<PlayerInput> _players = new List<PlayerInput>();
    // Assigned from Cameras associated with _players
    private readonly List<Camera> _cameras = new List<Camera>();
    
    // Represents every player in the game
    public List<PlayerController> Controllers = new List<PlayerController>();

    private int _active = -1;

    private PlayerInputManager _inputManager;

    private Mode _mode;
    public InputAction PlayerSwapAction;
    public InputAction ModeSwitchAction;

    private void Start()
    {
        _inputManager = GetComponent<PlayerInputManager>();
        
        foreach (PlayerController controller in Controllers)
        {
            _players.Add(controller.PlayerInput);
        }
        
        foreach (PlayerInput player in _players)
        {
            _cameras.Add(player.camera);
        }

        SetActivePlayer(0);

        _mode = Mode.Singleplayer;
        
        SetupActions();
    }

    private void SetupActions()
    {
        /*var map = new InputActionMap("Swap");
        SwapAction = map.AddAction("swap", binding: "<Keyboard>/1");
        SwitchAction = map.AddAction("switchMode", binding: "<Keyboard>/2");*/
        
        PlayerSwapAction.performed += context =>
        {
            CyclePlayers();
        };
        ModeSwitchAction.performed += context =>
        {
            // Switch modes
            _mode = 1 - _mode;
            switch (_mode)
            {
                case Mode.Multiplayer:
                    // In multiplayer mode, all controllers are active, and split-screen is enabled
                    // Cycling players is disabled in this mode
                    foreach ((PlayerController controller, Camera camera) in Controllers.Zip(_cameras, (controller, camera) => (controller, camera)))
                    {
                        controller.enabled = true;
                        camera.enabled = true;
                    }

                    _inputManager.splitScreen = true;
                    
                    PlayerSwapAction.Disable();
                    break;
                case Mode.Singleplayer:
                    // In singleplayer mode only one controller is active at a time (set with SetActivePlayer(playerIndex)), and split-screen is disabled
                    foreach ((PlayerController controller, Camera camera) in Controllers.Zip(_cameras, (controller, camera) => (controller, camera)))
                    {
                        controller.enabled = false;
                        camera.enabled = false;
                    }
                    
                    SetActivePlayer(0);
                    
                    _inputManager.splitScreen = false;
                    
                    PlayerSwapAction.Enable();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        };
        
        PlayerSwapAction.Enable();
        ModeSwitchAction.Enable();
    }

    private void SetActivePlayer(int index)
    {
        if (index >= Controllers.Count)
        {
            return;
        }

        if (_active == -1)
        {
            PlayerController newController = Controllers[index];
            var newCam = _cameras[index];
            
            newCam.enabled = true;
            newController.enabled = true;
        }
        else
        {
            int prev = _active;

            PlayerController prevController = Controllers[prev];
            PlayerController newController = Controllers[index];

            var prevCam = _cameras[prev];
            var newCam = _cameras[index];

            prevCam.enabled = false;
            newCam.enabled = true;
        
            prevController.enabled = false;
            newController.enabled = true;
        }
        
        
        _active = index;
    }
    
    private void CyclePlayers()
    {
        int current = _active;
        current++;
        if (current == Controllers.Count)
        {
            current = 0;
        }
        
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
}
