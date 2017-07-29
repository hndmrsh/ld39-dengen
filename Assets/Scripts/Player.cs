using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public string playerName;

    public Color controlledColour;
    public Color highlightColour;
    public Color placeableColour;

    public int TurnNumber { get; set; }
    public Hex HomeBaseTile { get; set; }

    public bool HasChosenHomeBaseLocation()
    {
        return TurnNumber > 0;
    }
}
