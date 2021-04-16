using System;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject Bob;
    public GameObject Hopsy;

    private PlayerInput player1;
    private PlayerInput player2;
    private PlayerInput activePlayer;

    private PlayerController p1Controller;
    private PlayerController p2Controller;
    private PlayerController activeController;

    private PlayerInputManager _inputManager;

    private Mode _mode;
    private InputAction swapAction;
    private InputAction switchAction;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = GetComponent<PlayerInputManager>();
        p1Controller = Bob.GetComponent<PlayerController>();
        player1 = p1Controller.playerInput;

        // To use any controller during singleplayer mode
        // Currently we assign these schemes since we don't have a menu that selects input
        player1.SwitchCurrentControlScheme("ArrowWASDGamepad");

        p2Controller = Hopsy.GetComponent<PlayerController>();
        player2 = p2Controller.playerInput;
        player2.SwitchCurrentControlScheme("ArrowWASDGamepad");

        activeController = p2Controller;
        activePlayer = player2;
        
        SetActivePlayer(p1Controller);

        _mode = Mode.Singleplayer;

        var map = new InputActionMap("Swap");
        swapAction = map.AddAction("swap", binding: "<Keyboard>/1");
        switchAction = map.AddAction("switchMode", binding: "<Keyboard>/2");
        
        swapAction.performed += context =>
        {
            SwapPlayers();
        };
        switchAction.performed += context =>
        {
            _mode = 1 - _mode;
            switch (_mode)
            {
                case Mode.Multiplayer:
                    p1Controller.enabled = true;
                    p2Controller.enabled = true;
                    player1.camera.enabled = true;
                    player2.camera.enabled = true;
                    player1.SwitchCurrentControlScheme("WASD");
                    player2.SwitchCurrentControlScheme("ArrowKeys");
                    _inputManager.splitScreen = true;
                    
                    swapAction.Disable();
                    break;
                case Mode.Singleplayer:
                    p1Controller.enabled = false;
                    p2Controller.enabled = false;
                    SwapPlayers();
                    player1.SwitchCurrentControlScheme("ArrowWASDGamepad");
                    player2.SwitchCurrentControlScheme("ArrowWASDGamepad");
                    _inputManager.splitScreen = false;
                    
                    swapAction.Enable();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        };
        
        swapAction.Enable();
        switchAction.Enable();
    }

    private void SetActivePlayer(PlayerController player)
    {
        Camera activeCam = activeController.playerInput.camera;
        Camera playerCam = player.playerInput.camera;
        activeCam.enabled = false;
        activeController.enabled = false;

        activeController = player;
        activePlayer = activeController.playerInput;
        activeController.enabled = true;
        playerCam.enabled = true;
    }
    
    private void SwapPlayers()
    {
        SetActivePlayer(p1Controller.enabled ? p2Controller : p1Controller);
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
