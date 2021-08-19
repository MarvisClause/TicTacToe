using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    #region Veriables 
    [SerializeField] protected GameObject _chooseMenu;
    [SerializeField] protected GameObject _gameArea;
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
    } 
    public void GameOver()
    {
       _gameArea.SetActive(false); 
       _chooseMenu.SetActive(true);
    }

    #endregion
}
