using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Player player1;
    public Player player2;

    public GameObject level;

    public Text instructionsText;

    void Start()
    {
        ChooseStartingLocation(player1);
    }

    private void ChooseStartingLocation(Player player)
    {
        instructionsText.color = player.highlightColour;
        instructionsText.text = player.playerName;



    }
}
