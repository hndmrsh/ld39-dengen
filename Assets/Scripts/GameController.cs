﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private enum TurnState
    {
        ChooseBase, SelectPiece, PlacePiece
    }

    private const string FIRST_TURN_INSTRUCTIONS = "Choose your starting position";
    private const string SELECT_PIECE_INSTRUCTIONS = "Select your next structure";
    private const string PLACE_PIECE_INSTRUCTIONS = "Place your structure";

    public const int DEAFULT_STARTING_POWER = 20;

    public Player player1;
    public Player player2;

    public GameObject player1Menu;
    public GameObject player2Menu;

    public GameObject gameOverPanel;

    public Color defaultColour;

    public GameObject level;

    public Text playerText;
    public Text instructionsText;

    public Player CurrentPlayer { get; set; }

    [Header("Structure templates")]
    public Structure homeBase;
    public Structure basicStructure;


    private TurnState turnState;
    private Structure selectedStructure;
    public Structure SelectedStructure
    {
        get
        {
            return selectedStructure;
        }
        set
        {
            selectedStructure = value;
            if (turnState != TurnState.PlacePiece)
            {
                StartPlacePieceTurn();
            }
        }
    }

    void Start()
    {
        CurrentPlayer = player1;
        NextTurn();
    }

    public void EndTurn()
    {
        this.selectedStructure = null;

        DeselectAll(player1Menu);
        DeselectAll(player2Menu);

        CalculateCosts(CurrentPlayer);

        CurrentPlayer.TurnNumber++;

        if (CurrentPlayer.RemainingPower <= 0)
        {
            SwitchPlayer();
            EndGame(CurrentPlayer);
        }
        else
        {
            SwitchPlayer();
            NextTurn();
        }
    }

    private void CalculateCosts(Player player)
    {
        if (player.HasChosenHomeBaseLocation)
        {
            foreach (Transform child in level.transform)
            {
                Hex hex = child.GetComponent<Hex>();
                if (hex && hex.ControllingPlayer == CurrentPlayer)
                {
                    player.RemainingPower -= hex.Structure.Cost;
                    hex.GetComponent<Renderer>().material.color = CurrentPlayer.placeableColour;
                }
            }
        }
    }

    private void SwitchPlayer()
    {
        if (CurrentPlayer == player1)
        {
            CurrentPlayer = player2;
        }
        else
        {
            CurrentPlayer = player1;
        }
    }

    private void DeselectAll(GameObject menu)
    {
        foreach (StructurePanel panel in menu.GetComponentsInChildren<StructurePanel>())
        {
            panel.Deselect();
        }
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
        turnState = TurnState.ChooseBase;

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
        turnState = TurnState.SelectPiece;

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
        turnState = TurnState.PlacePiece;

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

    private void EndGame(Player winner)
    {
        gameOverPanel.SetActive(true);

        Text winnerText = gameOverPanel.GetComponentInChildren<Text>();
        winnerText.text = winner.playerName.ToUpper() + " WINS";
        winnerText.color = winner.controlledColour;
    }

}