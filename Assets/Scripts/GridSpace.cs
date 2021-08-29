using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    #region Variables
    [SerializeField] private Button button;
    [SerializeField] private Text buttonText;

    private GameManager gameManager;
    #endregion 
    #region Methods 
    public void SetSpace()
    {
        if (gameManager._playerMove == true)
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
