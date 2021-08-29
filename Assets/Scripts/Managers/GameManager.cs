using UnityEngine;
using UnityEngine.UI; 

#region Player Class
[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button button;
}  
[System.Serializable]
public class PlayerColor
{
    public Color penelColor;
    public Color textColor;
}
#endregion 

#region Game Manager
public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text[] _buttonList;
    [SerializeField] private Player _playerX;
    [SerializeField] private Player _playerO;
    [SerializeField] private PlayerColor _activePlayerColor;
    [SerializeField] private PlayerColor _inactivePlayerColor;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _startInfo;
    
    private int _moveCount;

    private string _playerSide;
    private string _aiSide;
    public bool _playerMove;
    public float _delay;
    private int _value;
    #endregion

    #region Unity
    public void Awake()
    {
        SetGameManagerReferenceOnButtons();
        _gameOverPanel.SetActive(false);
        _moveCount = 0;
        _restartButton.SetActive(false);
        _playerMove = true;
    }
    private void Update()
    {
        if (_playerMove == false)
        {
            _delay += _delay * Time.deltaTime; 
            if (_delay >= 100)
            { 
                _value = Random.Range(0, 8);
                if (_buttonList[_value].GetComponentInParent<Button>().interactable == true)
                {
                    _buttonList[_value].text = GetAISide();
                    _buttonList[_value].GetComponentInParent<Button>().interactable = false;
                    EndTurn();
                }
            }
        }
    }
    #endregion

    #region Methods
    void SetGameManagerReferenceOnButtons()
    {
        for(int i = 0; i < _buttonList.Length; i++)
        {
            _buttonList[i].GetComponentInParent<GridSpace>().SetGameManagerReference(this);
        }
    }  


    public void  SetStartingSide(string startingSide)
    {
        _playerSide = startingSide;
        if (_playerSide == "X")
        {
            _aiSide = "O";
            SetPlayerColors(_playerX, _playerO);
        }
        else
        {
            _aiSide = "X";
            SetPlayerColors(_playerO, _playerX);
        }
        StartGame();
    }

    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        _startInfo.SetActive(false);
    }

    public string GetPlayerSide()
    {
        return _playerSide;
    }  
     
    public string GetAISide()
    {
        return _aiSide;
    }

    public void EndTurn()
    { 
        _moveCount++;
        // Player side
        if (_buttonList[0].text == _playerSide && _buttonList[1].text == _playerSide && _buttonList[2].text == _playerSide ||
            _buttonList[3].text == _playerSide && _buttonList[4].text == _playerSide && _buttonList[5].text == _playerSide ||
            _buttonList[6].text == _playerSide && _buttonList[7].text == _playerSide && _buttonList[8].text == _playerSide ||
            _buttonList[0].text == _playerSide && _buttonList[3].text == _playerSide && _buttonList[6].text == _playerSide ||
            _buttonList[1].text == _playerSide && _buttonList[4].text == _playerSide && _buttonList[7].text == _playerSide ||
            _buttonList[2].text == _playerSide && _buttonList[5].text == _playerSide && _buttonList[8].text == _playerSide ||
            _buttonList[0].text == _playerSide && _buttonList[4].text == _playerSide && _buttonList[8].text == _playerSide ||
            _buttonList[2].text == _playerSide && _buttonList[4].text == _playerSide && _buttonList[6].text == _playerSide)
        {
            GameOver(_playerSide);
        }
        
        // Ai side
        else if (_buttonList[0].text == _aiSide && _buttonList[1].text == _aiSide && _buttonList[2].text == _aiSide|| 
            _buttonList[3].text == _aiSide && _buttonList[4].text == _aiSide && _buttonList[5].text == _aiSide|| 
            _buttonList[6].text == _aiSide && _buttonList[7].text == _aiSide && _buttonList[8].text == _aiSide|| 
            _buttonList[0].text == _aiSide && _buttonList[3].text == _aiSide && _buttonList[6].text == _aiSide|| 
            _buttonList[1].text == _aiSide && _buttonList[4].text == _aiSide && _buttonList[7].text == _aiSide|| 
            _buttonList[2].text == _aiSide && _buttonList[5].text == _aiSide && _buttonList[8].text == _aiSide|| 
            _buttonList[0].text == _aiSide && _buttonList[4].text == _aiSide && _buttonList[8].text == _aiSide|| 
            _buttonList[2].text == _aiSide && _buttonList[4].text == _aiSide && _buttonList[6].text == _aiSide)
        {
            GameOver(_aiSide);
        }
        else if (_moveCount >= 9)
        {
            GameOver("draw");
        }
        else
        {
            ChangeSides();
        }
        
    }   


    void SetPlayerColors(Player newPlayer,Player oldPlayer)
    {
        newPlayer.panel.color = _activePlayerColor.penelColor;
        newPlayer.text.color = _activePlayerColor.textColor;
        oldPlayer.panel.color = _inactivePlayerColor.penelColor;
        oldPlayer.text.color = _inactivePlayerColor.textColor;
    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);
        if(winningPlayer == "draw")
        {
            SetGameOverText("It`s Draw!");
            SetPlayerColorsInactive();
        }
        else
        {
            SetGameOverText(winningPlayer + " Wins!");
        }
        _restartButton.SetActive(true);
    } 
    void ChangeSides()
    {
        //playerSide = (playerSide == "X") ? "0" : "X";
        _playerMove = (_playerMove == true) ? false : true;
        //if (playerSide == "X") 
        if(_playerMove==true)
        {
            SetPlayerColors(_playerX, _playerO);
        }
        else
        {
            SetPlayerColors(_playerO, _playerX);
        }
    } 
    void SetGameOverText(string value)
    {
        _gameOverPanel.SetActive(true);
        _gameOverText.text = value;
    } 
    public void RestartGame()
    {
        _moveCount = 0;
        _gameOverPanel.SetActive(false);
        _restartButton.SetActive(false);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        _startInfo.SetActive(true);
        _playerMove = true;
        _delay = 10; 

        for (int i = 0; i < _buttonList.Length; i++)
        {
            _buttonList[i].text = "";
        }
        
    } 

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < _buttonList.Length; i++)
        {
            _buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    } 
     
    void SetPlayerButtons(bool toggle)
    {
        _playerX.button.interactable = toggle;
        _playerO.button.interactable = toggle;
    }
     
    void SetPlayerColorsInactive()
    {
        _playerX.panel.color = _inactivePlayerColor.penelColor;
        _playerX.text.color = _inactivePlayerColor.textColor;
        _playerO.panel.color = _inactivePlayerColor.penelColor;
        _playerX.text.color = _inactivePlayerColor.textColor;
    }
    #endregion
}
#endregion