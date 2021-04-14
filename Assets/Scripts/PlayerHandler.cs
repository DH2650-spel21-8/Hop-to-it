using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    /*
     * Gameobject must be the players. 
     * They must have rigidbody, PlayerController script and Player Input component, set up with InputAction PlayerControls
     */
    public GameObject Bob;
    public GameObject Hopsy;

    private PlayerInput player1;
    private PlayerInput player2;

    private PlayerController p1Controller;
    private PlayerController p2Controller;

    private bool multiplayer;
    // Start is called before the first frame update
    void Start()
    {
        p1Controller = Bob.GetComponent<PlayerController>();
        player1 = p1Controller.playerInput;

        // To use any controller during singleplayer mode
        // Currently we assign these schemes since we don't have a menu that selects input
        player1.SwitchCurrentControlScheme("ArrowWASDGamepad");

        p2Controller = Hopsy.GetComponent<PlayerController>();
        player2 = p2Controller.playerInput;
        player2.SwitchCurrentControlScheme("ArrowWASDGamepad");

        p1Controller.enabled = true;
        p2Controller.enabled = false;

        multiplayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame || Keyboard.current.numpad1Key.wasPressedThisFrame)
        {
            // Yeah I know...  
            if (!multiplayer)
            {
                // Hey, it's only stupid if it doesn't work!
                p1Controller.enabled = !p1Controller.enabled;
                p2Controller.enabled = !p2Controller.enabled;
            }
        } else if (Keyboard.current.digit2Key.wasPressedThisFrame || Keyboard.current.numpad2Key.wasPressedThisFrame)
        {
            // Reset to singleplayer logic
            if (multiplayer)
            {
                p1Controller.enabled = true;
                p2Controller.enabled = false;
                player1.SwitchCurrentControlScheme("ArrowWASDGamepad");
                player2.SwitchCurrentControlScheme("ArrowWASDGamepad");
            }
            else
            {
                // Currently no way to determine input device. Until then, set different controlscheme
                p1Controller.enabled = true;
                p2Controller.enabled = true;
                player1.SwitchCurrentControlScheme("WASD");
                player2.SwitchCurrentControlScheme("ArrowKeys");
            }
            multiplayer = !multiplayer;
        }
    }
}
