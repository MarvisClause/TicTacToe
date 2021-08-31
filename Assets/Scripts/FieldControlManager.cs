using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for building and full control 
public class FieldControlManager : MonoBehaviour
{
    #region Events
    public event Action FieldIsMarked;

    #endregion

    #region Variables

    [Header(" FIELD BUILD BLOCKS ")]
    [SerializeField] private GameObject _markCell;
    [SerializeField] private GameObject _buildLine;

    private int _winRowQuant;
    public int WinRowQuant { get { return _winRowQuant; } }
    // field size 
    private int _fieldSize;
    // markField size
    private float _markFieldSizeOnScreen;

    // lines list
    private List<GameObject> _linesList;
    // List for mark cells
    private List<List<GameObject>> _gameObjectsMarksCells2DList;
    public List<List<GameObject>> GameObjectsMarksCells2DList { get { return _gameObjectsMarksCells2DList; } }

    // List for holds state of cells
    private List<List<MarkType>> _marksTypes2DList;
    public List<List<MarkType>> MarksTypes2DList { get { return _marksTypes2DList; } }

    // Turn state
    private MarkType _turnState;
    public  MarkType TurnState
        { get { return _turnState; }}

    public bool IsWinConditionReached { get; set; }

    // Input check
    private GameObject _firstInputCheckGameObject;
    private GameObject _secondInputCheckGameObject;

    #endregion

    #region Unity

    private void Awake()
    {
        // Initialize lists
        _linesList = new List<GameObject>();
        _gameObjectsMarksCells2DList = new List<List<GameObject>>();
        _marksTypes2DList = new List<List<MarkType>>();
    }

    private void OnDisable()
    {
        DisableField();
        // Disable inputs
        _firstInputCheckGameObject.SetActive(false);
        _secondInputCheckGameObject.SetActive(false);
    }

    #endregion

    #region Methods
    public void SetInputs(GameObject firstInputObject, GameObject secondInputObject)
    {
        // Check null referenced
        if (firstInputObject == null || secondInputObject == null)
        {
            Debug.LogWarning("Objects passed to the input of the field are null referenced!");
            return;
        }
        // Check gameobjects
        if (firstInputObject.GetComponent<InputCheck>() == null
            || secondInputObject.GetComponent<InputCheck>() == null)
        {
            Debug.LogWarning("Objects, which were passed to the input of the field do not have InputCheck inherited script!");
            return;
        }
        // Deactivate previous ones inputs
        if(_firstInputCheckGameObject != null || _secondInputCheckGameObject != null)
        {
            _firstInputCheckGameObject.SetActive(false);
            _secondInputCheckGameObject.SetActive(false);
        }
        _firstInputCheckGameObject = firstInputObject;
        _firstInputCheckGameObject.GetComponent<InputCheck>().SetInputToField(TurnState, this);
        _secondInputCheckGameObject = secondInputObject;
        _secondInputCheckGameObject.GetComponent<InputCheck>().SetInputToField(TurnState == MarkType.Cross ? MarkType.Circle : MarkType.Cross, this); 

        // invoke event
        FieldIsMarked?.Invoke();
    }

    public void CreateField(MarkType firstTurn = MarkType.Cross, int winRowQuant = 3, int fieldSize = 3,
        float markFieldSizeOnScreen = 2, float xCenterPos = 0, float yCenterPos = 0)
    {
        // Before creating new one disable field 
        DisableField(); 

        // Field parameters
        if (fieldSize < Globals.Min_Field_Size) { fieldSize = Globals.Min_Field_Size; }
        if (fieldSize > Globals.Max_Field_Size) { fieldSize = Globals.Max_Field_Size; }
        if (winRowQuant > fieldSize){winRowQuant = fieldSize; }
        _turnState = firstTurn;
        _winRowQuant = winRowQuant;
        _markFieldSizeOnScreen = markFieldSizeOnScreen;
        _fieldSize = fieldSize; 

        // Draw variables  
        float xLineStartPos = xCenterPos;
        float yLineStartPos = yCenterPos;
        int lineOffset = 0; 

        float adaptFieldSize = _markFieldSizeOnScreen / ((_markFieldSizeOnScreen * _fieldSize) / Globals.Min_Field_Size);
        adaptFieldSize = _markFieldSizeOnScreen * adaptFieldSize;
        // Getting start position 
        float xStartPos = xCenterPos - ((_fieldSize / 2) * adaptFieldSize);
        float yStartPos = yCenterPos + ((_fieldSize / 2) * adaptFieldSize);
        // Field adaptability
        if (_fieldSize % 2 == 0)
        {
            // Ñorrect visualization
            xStartPos += adaptFieldSize / 2;
            yStartPos -= adaptFieldSize / 2;
        }
        else
        {
            // Moving start position of lines drawing for adaptability
            xLineStartPos -= adaptFieldSize / 2;
            yLineStartPos += adaptFieldSize / 2;
            lineOffset = 1;
        }
        lineOffset = (_fieldSize - 1) / 2 - lineOffset;
        xLineStartPos -= adaptFieldSize* lineOffset;
        yLineStartPos += adaptFieldSize * lineOffset;
        // Draw vertical lines
        for (int lineIt = 0; lineIt < _fieldSize - 1; lineIt++)
        {
            // Spawn line
            GameObject spawnedXLine = SpawnManager.GetInstance().SpawnObject(SpawnManager.PoolType.BuildLine, _buildLine);
            GameObject spawnedYline = SpawnManager.GetInstance().SpawnObject(SpawnManager.PoolType.BuildLine, _buildLine);
            // Scale object
            spawnedXLine.transform.localScale = new Vector3(adaptFieldSize / 4 / 3, adaptFieldSize * _fieldSize);
            spawnedYline.transform.localScale = new Vector3(spawnedXLine.transform.localScale.y, spawnedXLine.transform.localScale.x);
            // Setting position of the line
            spawnedXLine.transform.position = new Vector3(xLineStartPos + (lineIt * adaptFieldSize), yCenterPos);
            spawnedYline.transform.position = new Vector3(xCenterPos, yLineStartPos - (lineIt * adaptFieldSize));
            // Add lines to the list
            _linesList.Add(spawnedXLine);
            _linesList.Add(spawnedYline);
        }
        // Create field, depending on the entered size
        for (int y = 0; y < _fieldSize; y++)
        {
            // Add new sub list 
            _gameObjectsMarksCells2DList.Add(new List<GameObject>());
            _marksTypes2DList.Add(new List<MarkType>());
            for (int x = 0; x < _fieldSize; x++)
            {
                // Spawn mark cell
                GameObject spawnedMarkCell = SpawnManager.GetInstance().SpawnObject(SpawnManager.PoolType.MarkCell, _markCell);
                // Rescale object
                spawnedMarkCell.transform.localScale = new Vector3(adaptFieldSize, adaptFieldSize);
                // Set it on new position
                spawnedMarkCell.transform.position = new Vector2(xStartPos + x * spawnedMarkCell.transform.localScale.x, yStartPos - y * spawnedMarkCell.transform.localScale.y);
                // Add cell to list
                _gameObjectsMarksCells2DList[y].Add(spawnedMarkCell);
                // Add mark state
                _marksTypes2DList[y].Add(MarkType.Empty);
            }
        }
        // Invoke event
        FieldIsMarked?.Invoke();
    }

    public void DisableField()
    {
        // Clear all lists
        if (_linesList == null ||
            _gameObjectsMarksCells2DList == null ||
            _marksTypes2DList == null)
        {
            return;
        }
        _linesList.Clear();
        _gameObjectsMarksCells2DList.Clear();
        _marksTypes2DList.Clear();
        // Disable all squares and lines
        for (int i = 0; i < _linesList.Count; i++)
        {
            _linesList[i].SetActive(false);
        }
        for (int i = 0; i < _gameObjectsMarksCells2DList.Count; i++)
        {
            for (int j = 0; j < _gameObjectsMarksCells2DList.Count; j++)
            {
                _gameObjectsMarksCells2DList[i][j].SetActive(false);
            }
        }
    }

    private bool CheckFieldForWinCondition()
    {
        // Variable for storing max amount of marks
        int maxQuantityOfMarks = 0;
        for (int i = 0; i < _gameObjectsMarksCells2DList.Count; i++)
        {
            for (int j = 0; j < _gameObjectsMarksCells2DList.Count; j++)
            {
                if (CheckPointForWinCondition(i, j, _marksTypes2DList[i][j], ref maxQuantityOfMarks) == true)
                {
                    return true;
                }
            }
        }
        return false;
    } 

    public bool CheckPointForWinCondition(int y, int x, MarkType markType, ref int maxMarkCount,
        int markCount = 1, int nextX = 0, int nextY = 0, bool isReverse = false)
    {
        // CHECK FOR CORRECTNESS X Y
        if (y < 0 || y >= _marksTypes2DList.Count
            || x < 0 || x >= _marksTypes2DList.Count)
        {
            return false;
        }
        if (markType == MarkType.Empty || _marksTypes2DList[y][x] != markType)
        {
            return false;
        }
        if (nextX == 0 && nextY == 0)
        {
            // Variable for storing max amount of marks, which go through this point
            maxMarkCount = 1;
            for (int yIt = y - 1; yIt <= y + 1; yIt++)
            {
                for (int xIt = x - 1; xIt <= x + 1; xIt++)
                {
                    // Ignore same point as ours
                    if (xIt == x && yIt == y) { continue; }
                    // Check, if we are in the boundaries
                    if (yIt >= 0 && yIt < _marksTypes2DList.Count
                        && xIt >= 0 && xIt < _marksTypes2DList.Count)
                    {
                        // Check next point for win condition
                        if (CheckPointForWinCondition(yIt, xIt, markType,ref maxMarkCount, markCount + 1, xIt - x, yIt - y) == true)
                        {
                            Debug.Log("(Possible) Win condition from point [" + y + "][" + x + "] towards point"
                                + "[" + yIt + "]" + "[" + xIt + "]" + " with " + maxMarkCount + " marks");
                            return true;
                        }
                    }
                }
            }
        }
        // go to the destination 
        else
        {
            if (markCount >= maxMarkCount)
            {
                maxMarkCount = markCount;
            }
            // Win check
            if (markCount == _winRowQuant)
            {
                return true;
            }
            // move on
            else
            {
                // Marks check for forward and backwards 
                bool forwardBackwardCheck;
                // Forward check
                forwardBackwardCheck = CheckPointForWinCondition(y + nextY, x + nextX, markType, ref maxMarkCount, markCount + 1, nextX, nextY, isReverse);
                if (forwardBackwardCheck == false && isReverse == false)
                {
                    // Decrease win count, because we go backwards
                    markCount = 1;
                    // Check backwards
                    forwardBackwardCheck = CheckPointForWinCondition(y - nextY, x - nextX, markType, ref maxMarkCount, markCount + 1, -nextX, -nextY, true);
                }
                // Return result
                return forwardBackwardCheck;
            }
        }
        return false;
    }

    public void MarkCellInField(GameObject markCell)
    {
        // Win check
        if (IsWinConditionReached == true)
        {
            return;
        } 
        MarkCell markCellScript = null;
        // Checking, if there is already some mark on this cell
        try
        {
            markCellScript = markCell.GetComponent<MarkCell>();
            if (markCellScript.MarkType != MarkType.Empty)
            {
                return;
            }
        }
        catch (Exception error)
        {
            Debug.LogError(error.Message);
        }
        // Variable for storing max amount of marks, which go through this point
        int maxQuantityOfMarks = 1;
        // Looking for passed gameobject in list
        for (int i = 0; i < _gameObjectsMarksCells2DList.Count; i++)
        {
            for (int j = 0; j < _gameObjectsMarksCells2DList.Count; j++)
            {
                if (markCell.Equals(_gameObjectsMarksCells2DList[i][j]))
                {
                    try
                    {
                        // Changing mark type of the cell according to turn state
                        markCellScript.MarkType = TurnState;
                        _marksTypes2DList[i][j] = _turnState;
                        // Checking win condition
                        IsWinConditionReached = CheckPointForWinCondition(i, j, TurnState, ref maxQuantityOfMarks);
                        if (IsWinConditionReached == true)
                        {
                            // Win
                            Debug.Log(TurnState.ToString() + " has won!");
                            return;
                        }
                        Debug.Log(TurnState.ToString() + " in a row: " + maxQuantityOfMarks);
                        // Changing next turn mark
                        if (_turnState == MarkType.Cross) { _turnState = MarkType.Circle; }
                        else { _turnState = MarkType.Cross; }
                        // Calling event
                        FieldIsMarked?.Invoke();
                        return;
                    }
                    catch (Exception error)
                    {
                        Debug.LogError(error.Message);
                    }
                }
            }
        }
    }

    #endregion
}
