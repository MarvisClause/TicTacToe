using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    #region Variables
    public Button button;
    public Text buttonText;

    private GameManager gameManager;
    #endregion 
    #region Methods 
    public void SetSpace()
    {
        if (gameManager.playerMove == true)
        {
            buttonText.text = gameManager.GetPlayerSide();
            button.interactable = false;
            gameManager.EndTurn();
        }
    } 

    public void SetGameManagerReference(GameManager manager)
    {
        gameManager = manager;
    }
    #endregion
}
