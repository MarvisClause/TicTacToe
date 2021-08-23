using System;
using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables   
    // X=1 AND O=0 
    [SerializeField] protected int whoseTurn;  
    // Counts The number of Turn played
    [SerializeField] protected int turnCounter; 
    // Displays whos turn it is
    [SerializeField] GameObject[] turnIcons; 
    // X=0 icon and 0=1 icon
    [SerializeField] Sprite[] playerIcons; 
    // Playable space for our game
    [SerializeField] Button[] ticTacToeSpaces;
    // ID's which space was marked by which player
    [SerializeField] protected int[] markedSpaces;
    // Hold the text component of the winner text
    [SerializeField] Text winnerText;
    // Hold all the difrent line for show that ther is a winner
    [SerializeField] GameObject[] winningLine; 
    // Helping make game over area be more clear
    [SerializeField] GameObject winnerPanel;
    public Button xPlayerBtn;
    public Button oPlayerBtn;

    #endregion
    #region Unity
    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
    #region Methods  
    void GameSetup()
    {
        whoseTurn = 0;
        turnCounter = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false); 
        for(int i=0;i<ticTacToeSpaces.Length;i++)
        {
            ticTacToeSpaces[i].interactable = true;
            ticTacToeSpaces[i].GetComponent<Image>().sprite = null;
        } 
        for(int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
    } 
    public void TicTacToeButton(int WhichNumber) 
    { 
        ticTacToeSpaces[WhichNumber].image.sprite = playerIcons[whoseTurn];
        ticTacToeSpaces[WhichNumber].interactable = false;

        markedSpaces[WhichNumber] = whoseTurn+1;
        turnCounter++;
        if (turnCounter > 4)
        {
            WinnerCheck();
        }
        

        if (whoseTurn == 0)
        {
            whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        } 
    } 
    void WinnerCheck()
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];// \
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];// |

        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 }; 

        for (int i = 0; i < solutions.Length; i++)
        {
            // Circle win
            /* if (solutions[i] == 3 * Convert.ToInt32(whoseTurn == 1))
             {
                 Debug.Log("Player " + whoseTurn + " won!");
                 return;
             }
             // Cross win
             if (solutions[i] == 3 * Convert.ToInt32(whoseTurn == 0))
             {
                 Debug.Log("Player " + whoseTurn + " won!");
                 return;
             }*/
            if (solutions[i] == 3*(whoseTurn + 1))
            {
                WinnerDisplay(i);
                //Debug.Log("Player " + whoseTurn + " won!");
                return;
            }
        } 

    }
    public void WinnerDisplay(int indexIn)
    {
        winnerPanel.gameObject.SetActive(true);
        if (whoseTurn == 0)
        {
            winnerText.text = "Player X Wins!";
        }
        else if (whoseTurn == 1)
        {
            winnerText.text = "Player 0 Wins !";
        }
        winningLine[indexIn].SetActive(true);  
    } 
    public void Rematch()
    {
        GameSetup(); 
        for (int i =0;i<winningLine.Length;i++)
        {
            winningLine[i].SetActive(false); 
        }
        winnerPanel.SetActive(false);
    } 
    public void SwitchPlayer(int witchPlayer)
    {
        if (witchPlayer == 0)
        {
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        else if (witchPlayer == 1)
        {
            whoseTurn = 1;
            turnIcons[1].SetActive(true);
            turnIcons[0].SetActive(false);
        }
    }
    #endregion
}
