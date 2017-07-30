using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{

    public Player isPossibleStartingPositionForPlayer;
    public GameController gameController;

    public GameObject[] triggers;

    public bool CurrentlySelectable { get; set; }
    public Player ControllingPlayer { get; set; }
    public Structure Structure { get; set; }

    private List<Hex> neighbours;

    void Start()
    {
        neighbours = new List<Hex>();
    }

    private void OnMouseEnter()
    {
        if (CurrentlySelectable)
        {
            GetComponent<Renderer>().material.color = gameController.CurrentPlayer.highlightColour;
        }
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = GetNonHighlightedColour();
    }

    private void OnMouseDown()
    {
        if (CurrentlySelectable)
        {
            if (!gameController.CurrentPlayer.HasChosenHomeBaseLocation)
            {
                gameController.CurrentPlayer.HomeBaseTile = this;
                Structure = gameController.homeBase;
                BuildStructure(gameController.homeBase);
            } else if (gameController.SelectedStructure)
            {
                Structure = gameController.SelectedStructure;
                BuildStructure(gameController.SelectedStructure);
            }
        }
    }

    void BuildStructure(Structure structure)
    {
        ControllingPlayer = gameController.CurrentPlayer;
        structure = GameObject.Instantiate(structure.gameObject, transform).GetComponent<Structure>();

        GetComponent<Renderer>().material.color = GetNonHighlightedColour();

        if (gameController.CurrentPlayer == gameController.player1)
        {
            structure.transform.Rotate(Vector3.forward * 180);
        }

        gameController.EndTurn();
    }

    public Color GetNonHighlightedColour()
    {
        if (ControllingPlayer)
        {
            return ControllingPlayer.controlledColour;
        }
        else
        {
            if (CurrentlySelectable)
            {
                return gameController.CurrentPlayer.placeableColour;
            }
            else
            {
                return gameController.defaultColour;
            }
        }
    }

    // called on scene creation to build list of neighbouring tiles
    private void OnTriggerEnter(Collider other)
    {
        neighbours.Add(other.GetComponent<Hex>());
    }

    public bool IsSelectableByPlayer(Player player)
    {
        foreach (Hex neighbour in neighbours)
        {
            if (neighbour && neighbour.ControllingPlayer == player)
            {
                return true;
            }
        }

        return false;
    }
}
