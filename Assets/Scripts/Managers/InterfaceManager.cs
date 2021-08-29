using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    #region Veriables 
    [SerializeField] protected GameObject _MainMenu;
    [SerializeField] protected GameObject _hud;
    #endregion
    public void StartGame()
    {
        _MainMenu.SetActive(false);
        _hud.SetActive(true);
    }
    public void GameOver()
    {
        _MainMenu.SetActive(true);
        _hud.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
