using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{

    public Player isPossibleStartingPositionForPlayer;
    public int bonus = 0;
    public GameController gameController;

    public GameObject[] triggers;

    public bool CurrentlySelectable { get; set; }
    public Player ControllingPlayer { get; set; }
    public Structure Structure { get; set; }

    private List<Hex> neighbours;

    private int ownerInfluence;
    private int attackerInfluence;

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

    public void UpdateInfluences()
    {
        ownerInfluence = 0;
        attackerInfluence = 0;

        // we only calculate influence on tiles which have an owner, as only they can be attacked
        if (ControllingPlayer)
        {
            // ownerInfluence += Structure.Cost;

            foreach (Hex neighbour in neighbours)
            {
                if (neighbour && neighbour.ControllingPlayer)
                {
                    if (neighbour.ControllingPlayer == ControllingPlayer)
                    {
                        ownerInfluence += neighbour.Structure.Cost;
                    }
                    else
                    {
                        attackerInfluence += neighbour.Structure.Cost;
                    }
                }
            }
        }

        
    }

    public bool IsSelectableByPlayer(Player player)
    {
        foreach (Hex neighbour in neighbours)
        {
            if (neighbour)
            {
                if ((!Structure && neighbour.ControllingPlayer == player) ||
                    (Structure && neighbour.ControllingPlayer != player && CanAttackSpace(player)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool CanAttackSpace(Player player)
    {
        return player != ControllingPlayer && attackerInfluence > ownerInfluence;
    }
}
