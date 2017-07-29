using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private const string FIRST_TURN_INSTRUCTIONS = "Choose your starting position";
    private const string SELECT_PIECE_INSTRUCTIONS = "Select your next structure";
    private const string PLACE_PIECE_INSTRUCTIONS = "Place your structure";

    public Player player1;
    public Player player2;

    public Color defaultColour;

    public GameObject level;

    public Text playerText;
    public Text instructionsText;

    public Player CurrentPlayer { get; set; }

    [Header("Structure templates")]
    public Structure homeBase;
    public Structure basicWorker;


    void Start()
    {
        CurrentPlayer = player1;
        NextTurn();
    }

    public void EndTurn()
    {
        CurrentPlayer.TurnNumber++;

        if (CurrentPlayer == player1)
        {
            CurrentPlayer = player2;
        }
        else
        {
            CurrentPlayer = player1;
        }

        NextTurn();
    }

    private void NextTurn()
    {
        instructionsText.color = CurrentPlayer.highlightColour;
        playerText.color = CurrentPlayer.controlledColour;
        playerText.text = CurrentPlayer.playerName;

        if (CurrentPlayer.TurnNumber == 0)
        {
            StartChooseStartingLocationTurn();
        }
        else
        {
            StartSelectPieceTurn();
        }
    }

    private void StartChooseStartingLocationTurn()
    {
        instructionsText.text = FIRST_TURN_INSTRUCTIONS;

        foreach (Transform child in level.transform)
        {
            Hex hex = child.GetComponent<Hex>();
            if (hex && hex.isPossibleStartingPositionForPlayer == CurrentPlayer)
            {
                hex.CurrentlySelectable = true;
                hex.GetComponent<Renderer>().material.color = CurrentPlayer.placeableColour;
            }
            else
            {
                hex.CurrentlySelectable = false;
                hex.GetComponent<Renderer>().material.color = hex.GetNonHighlightedColour();
            }
        }
    }

    private void StartSelectPieceTurn()
    {
        instructionsText.text = SELECT_PIECE_INSTRUCTIONS;

        foreach (Transform child in level.transform)
        {
            Hex hex = child.GetComponent<Hex>();
            hex.CurrentlySelectable = false;
            hex.GetComponent<Renderer>().material.color = hex.GetNonHighlightedColour();
        }
    }

    private void StartPlacePieceTurn()
    {
        instructionsText.text = PLACE_PIECE_INSTRUCTIONS;

        foreach (Transform child in level.transform)
        {
            Hex hex = child.GetComponent<Hex>();
            hex.CurrentlySelectable = hex.IsSelectableByPlayer(CurrentPlayer);

            if (hex.CurrentlySelectable)
            {
                hex.GetComponent<Renderer>().material.color = CurrentPlayer.placeableColour;
            }
            else
            {
                hex.GetComponent<Renderer>().material.color = hex.GetNonHighlightedColour();
            }
        }
    }

}