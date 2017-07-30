using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructurePanel : MonoBehaviour
{

    public GameController gameController;
    public Text costText;
    public Player owner;
    public Structure template;

    public bool IsSelectable
    {
        get
        {
            return gameController.CurrentPlayer == owner && owner.HasChosenHomeBaseLocation;
        }
    }

    private void Start()
    {
        if (template)
        {
            costText.text = (-template.Cost).ToString();
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
            if (!IsSkipTurnButton() && gameController.SelectedStructure == template)
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
            if (!IsSkipTurnButton())
            {
                gameController.SelectedStructure = template;
            }
            else
            {
                gameController.EndTurn();
            }
        }
    }

    public void Deselect()
    {
        GetComponent<Image>().color = owner.menuUnhighlightedColour;
    }

    private bool IsSkipTurnButton()
    {
        return !template;
    }

}
