using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{

    public enum Mode
    {
        Multiplayer = 1,
        Singleplayer = 0
    }
    /*
     * Gameobject must be the players. 
     * They must have rigidbody, PlayerController script and Player Input component, set up with InputAction PlayerControls
     */

    private readonly List<PlayerInput> _players = new List<PlayerInput>();
    private readonly List<Camera> _cameras = new List<Camera>();
    public List<PlayerController> Controllers = new List<PlayerController>();

    private int _active = 0;

    private PlayerInputManager _inputManager;

    private Mode _mode;
    private InputAction _swapAction;
    private InputAction _switchAction;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = GetComponent<PlayerInputManager>();
        
        foreach (PlayerController controller in Controllers)
        {
            _players.Add(controller.PlayerInput);
        }

        // To use any controller during singleplayer mode
        // Currently we assign these schemes since we don't have a menu that selects input
        foreach (PlayerInput player in _players)
        {
            _cameras.Add(player.camera);
        }

        _active = Controllers.Count - 1;
        SetActivePlayer(0);

        _mode = Mode.Singleplayer;
        
        SetupActions();
    }

    private void SetupActions()
    {
        var map = new InputActionMap("Swap");
        _swapAction = map.AddAction("swap", binding: "<Keyboard>/1");
        _switchAction = map.AddAction("switchMode", binding: "<Keyboard>/2");
        
        _swapAction.performed += context =>
        {
            CyclePlayers();
        };
        _switchAction.performed += context =>
        {
            _mode = 1 - _mode;
            switch (_mode)
            {
                case Mode.Multiplayer:

                    foreach ((PlayerController controller, Camera camera) in Controllers.Zip(_cameras, (controller, camera) => (controller, camera)))
                    {
                        controller.enabled = true;
                        camera.enabled = true;
                    }

                    _inputManager.splitScreen = true;
                    
                    _swapAction.Disable();
                    break;
                case Mode.Singleplayer:
                    foreach ((PlayerController controller, Camera camera) in Controllers.Zip(_cameras, (controller, camera) => (controller, camera)))
                    {
                        controller.enabled = false;
                        camera.enabled = false;
                    }
                    
                    SetActivePlayer(0);
                    
                    _inputManager.splitScreen = false;
                    
                    _swapAction.Enable();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        };
        
        _swapAction.Enable();
        _switchAction.Enable();
    }

    private void SetActivePlayer(int index)
    {
        if (index >= Controllers.Count)
        {
            return;
        }

        int prev = _active;

        PlayerInput prevPlayer = _players[prev];
        PlayerInput newPlayer = _players[index];
        
        PlayerController prevController = Controllers[prev];
        PlayerController newController = Controllers[index];

        var prevCam = _cameras[prev];
        var newCam = _cameras[index];

        prevCam.enabled = false;
        newCam.enabled = true;
        
        prevController.enabled = false;
        newController.enabled = true;
        
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
