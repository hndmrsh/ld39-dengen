using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Player player1;
    public Player player2;
    
    public Color defaultColour;

    public GameObject level;

    public Text playerText;
    public Text instructionsText;

    public Player CurrentPlayer { get; set; }


    void Start()
    {
        CurrentPlayer = player1;
        NextTurn();
    }
    
    private void ChooseStartingLocation()
    {
        instructionsText.color = CurrentPlayer.highlightColour;
        playerText.color = CurrentPlayer.controlledColour;
        playerText.text = CurrentPlayer.playerName;

        foreach (Transform child in level.transform)
        {
            Hex hex = child.GetComponent<Hex>();
            if (hex && hex.isPossibleStartingPositionForPlayer == CurrentPlayer)
            {
                hex.CurrentlySelectable = true;
                hex.GetComponent<Renderer>().material.color = CurrentPlayer.startingAreaColour;
            } else
            {
                hex.CurrentlySelectable = false;
            }
        }
    }

    void EndTurn()
    {
        if (CurrentPlayer == player1)
        {
            CurrentPlayer = player2;
        } else
        {
            CurrentPlayer = player1;
        }

        NextTurn();
    }

    private void NextTurn()
    {
        if (CurrentPlayer.TurnNumber == 0)
        {
            ChooseStartingLocation();
        }
    }
    
}
