using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructurePanel : MonoBehaviour
{

    public GameController gameController;
    public Player owner;
    public Structure template;

    public bool IsSelectable
    {
        get
        {
            return gameController.CurrentPlayer == owner && owner.HasChosenHomeBaseLocation;
        }
    }

    public void PointerEnter()
    {
        if (IsSelectable)
        {
            GetComponent<Image>().color = owner.highlightColour;
        }
    }

    public void PointerExit()
    {
        if (IsSelectable)
        {
            if (gameController.SelectedStructure == template)
            {
                GetComponent<Image>().color = owner.controlledColour;
            }
            else
            {
                GetComponent<Image>().color = owner.menuUnhighlightedColour;
            }
        }
    }

    public void PointerClick()
    {
        if (IsSelectable)
        {
            gameController.SelectedStructure = template;
        }
    }

    public void Deselect()
    {
        GetComponent<Image>().color = owner.menuUnhighlightedColour;
    }

}
