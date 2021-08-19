using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables   
    // X=1 AND O=0 
    [SerializeField] protected int _whoseTurn;  
    // Counts The number of Turn played
    [SerializeField] protected int _turnCounter; 
    // Displays whos turn it is
    [SerializeField] GameObject[] turnIcons; 
    // X=0 icon and 0=1 icon
    [SerializeField] Sprite[] playerIcons; 
    // Playable space for our game
    [SerializeField] Button[] ticTacToeSpaces;
    // ID's which space was marked by which player
    [SerializeField] protected int[] markedSpaces;
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
        _whoseTurn = 0;
        _turnCounter = 0;
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
        ticTacToeSpaces[WhichNumber].image.sprite = playerIcons[_whoseTurn];
        ticTacToeSpaces[WhichNumber].interactable = false;

        markedSpaces[WhichNumber] = _whoseTurn+1;
        _turnCounter++;

        if (_turnCounter > 4)
        {
            WinnerCheck();
        }

        if (_whoseTurn == 0)
        {
            _whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            _whoseTurn = 0;
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
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[0] + markedSpaces[4] + markedSpaces[6];

        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 }; 

        for (int i = 0; i < solutions.Length; i++)
        {
            if(solutions[i]== 3 * (_whoseTurn = 1))
            {
                Debug.Log("Player " + _whoseTurn + " won!");
                return;
            }
        }
    }
    #endregion
}
