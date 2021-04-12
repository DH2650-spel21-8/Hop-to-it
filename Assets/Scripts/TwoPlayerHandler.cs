using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerHandler : MonoBehaviour
{
    private PlayerMovementV1 player1Input;
    private PlayerMovementV1 player2Input;
    private bool multiplayer;


    // Start is called before the first frame update
    void Start()
    {      
        player1Input = GameObject.Find("Player1").GetComponent<PlayerMovementV1>();
        player2Input = GameObject.Find("Player2").GetComponent<PlayerMovementV1>();

        if (!multiplayer)
            player2Input.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Enable/Disable multiplayer
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown("2"))
        {
            Multiplayer(!multiplayer);
        }

        else if (multiplayer)
        {
            Multiplayer(multiplayer);
        }
        // Swap between characters in single player mode
        else if (!multiplayer && (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown("1")))
        {
            Debug.Log("Swapped character");
            player1Input.enabled = !player1Input.enabled;
            player2Input.enabled = !player2Input.enabled;
        }
    }

    // Enable or disable multiplayer
    private void Multiplayer(bool enable)
    {
        if (enable)
        {
            player1Input.EnableMultiplayer();
            player2Input.EnableMultiplayer();
            player2Input.SetPlayerTwo(true);
            player2Input.enabled = true; 
            player1Input.enabled = true;
        }
        else
        {
            player1Input.DisableMultiplayer();
            player2Input.DisableMultiplayer();
            player2Input.SetPlayerTwo(false);
            player2Input.enabled = false;
        }
        multiplayer = enable;
    }
}
