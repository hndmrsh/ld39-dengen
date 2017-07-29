using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour {

    public Player isPossibleStartingPositionForPlayer;
    public GameController gameController;

    public bool CurrentlySelectable { get; set; }

    private Color cachedMaterialColour;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        cachedMaterialColour = GetComponent<Renderer>().material.color;

        if (CurrentlySelectable)
        {
            GetComponent<Renderer>().material.color = gameController.CurrentPlayer.highlightColour;
        }
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = cachedMaterialColour;
    }
}
