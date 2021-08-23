using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    #region Veriables 
    [SerializeField] protected GameObject _chooseMenu;
    [SerializeField] protected GameObject _gameArea;
    [SerializeField] protected GameObject _hud;
    //[SerializeField] protected GameObject _orderOfMoves;
    //[SerializeField] protected GameObject _winningLine;
    //[SerializeField] protected GameObject _winnerPanel; 
    [SerializeField] protected GameObject _gameOver;
    #endregion
    #region Unity
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    #endregion
    #region Methods  
     
    public void StartGame()
    {
        _chooseMenu.SetActive(false);
        _gameArea.SetActive(true);
        _hud.SetActive(true);
        _gameOver.SetActive(true);
        
        
    } 
    public void GameOver()
    {
       _gameArea.SetActive(false); 
       _chooseMenu.SetActive(true);
       _hud.SetActive(false);
       _gameOver.SetActive(true);
    } 

     
    
    #endregion
}
