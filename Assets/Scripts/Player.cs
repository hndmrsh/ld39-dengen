using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public string playerName;

    public Color controlledColour;
    public Color highlightColour;
    public Color placeableColour;
    public Color menuUnhighlightedColour;

    public Text remainingPowerText;

    public int TurnNumber { get; set; }
    public Hex HomeBaseTile { get; set; }

    private int remainingPower;
    public int RemainingPower {
        get
        {
            return remainingPower;
        }

        set
        {
            remainingPower = value;
            remainingPowerText.text = value.ToString();
        }
    }
    
    public bool HasChosenHomeBaseLocation
    {
        get
        {
            return TurnNumber > 0;
        }
    }

    private void Start()
    {
        RemainingPower = GameController.DEAFULT_STARTING_POWER;

    }


}
