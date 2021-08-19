using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables   
    // X=1 AND O=0 
    [SerializeField] protected int _whoTurn;  
    // Counts The number of Turn played
    [SerializeField] protected int _turnCounter; 
    // Displays whos turn it is
    [SerializeField] GameObject[] turnIcons; 
    // X=0 icon and 0=1 icon
    [SerializeField] Sprite[] playerIcons; 
    // Playable space for our game
    [SerializeField] Button[] ticTacToeSpaces;
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
        _whoTurn = 0;
        _turnCounter = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false); 
        for(int i=0;i<ticTacToeSpaces.Length;i++)
        {
            ticTacToeSpaces[i].interactable = true;
            ticTacToeSpaces[i].GetComponent<Image>().sprite = null;
        }
    }
    #endregion
}
