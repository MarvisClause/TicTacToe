using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for AI input
public class InputAI : InputCheck
{
    // this struct helps AI to decide, which cells are more importnat to mark
    public struct PriorityList
    {
        // modifies the variable depending on the priority cell
        public int priorityLevel; 
        
        // List of cells with priorities.
        // If the priority is exceeded, the sheet will be cleared and filled with these cells
        public List<GameObject> markCellList;
    }

    #region Variables

    //  list of priority
    private PriorityList _priorityList;

    #endregion

    #region Unity

    private void OnDisable()
    {
        // Unsubscribe
        _fieldControl.FieldIsMarked -= AnalyzeAndMarkField;
    }

    #endregion

    #region Methods
    public override void SetInputToField(MarkType inputMarkType, FieldControlManager fieldControl)
    {
        base.SetInputToField(inputMarkType, fieldControl); 

        // event subscribe
        _fieldControl.FieldIsMarked += AnalyzeAndMarkField;
    }

    // Analyze the field and mark the highest priority cells
    private void AnalyzeAndMarkField()
    {
        // if not the user's turn
        if (_userMarkType != _fieldControl.TurnState) { return; } 

        // Ñomparing priority level
        int tempPriorityLevel = 0;
       
        _priorityList = new PriorityList();
        _priorityList.priorityLevel = tempPriorityLevel;
        _priorityList.markCellList = new List<GameObject>();

        // cell analysis
        for (int i = 0; i < _fieldControl.MarksTypes2DList.Count; i++)
        {
            for (int j = 0; j < _fieldControl.MarksTypes2DList.Count; j++)
            {
                // cell is not empty
                if  (_fieldControl.MarksTypes2DList[i][j] != MarkType.Empty)
                {
                    continue;
                } 

                // user = 0
                // ai = 1 

                for (int check = 0; check < 2; check++)
                {
                    
                    if (check == 0)
                    {
                        _fieldControl.MarksTypes2DList[i][j] = _userMarkType == MarkType.Cross ? MarkType.Circle : MarkType.Cross;
                    }
                    else
                    {
                        _fieldControl.MarksTypes2DList[i][j] = _userMarkType;
                    } 

                    // start analyze
                    _ = _fieldControl.CheckPointForWinCondition(i, j, _fieldControl.MarksTypes2DList[i][j], ref tempPriorityLevel); 

                    // update the list
                    if (tempPriorityLevel > _priorityList.priorityLevel)
                    {
                        _priorityList.priorityLevel = tempPriorityLevel;
                        _priorityList.markCellList.Clear();
                        _priorityList.markCellList.Add(_fieldControl.GameObjectsMarksCells2DList[i][j]);
                    } 

                    // if priority level is the same, add this cell 
                    else if (tempPriorityLevel == _priorityList.priorityLevel)
                    {
                        _priorityList.markCellList.Add(_fieldControl.GameObjectsMarksCells2DList[i][j]);
                    } 

                    // if priority level is the same we check cells with our mark then mark this cell immediately
                    else if (tempPriorityLevel == _fieldControl.WinRowQuant && check == 1)
                    {
                        // mark this field as empty
                        _fieldControl.MarksTypes2DList[i][j] = MarkType.Empty; 

                        // actually mark it
                        _fieldControl.MarkCellInField(_fieldControl.GameObjectsMarksCells2DList[i][j]); 

                        // end function
                        return;
                    }
                    // Set field empty 
                    _fieldControl.MarksTypes2DList[i][j] = MarkType.Empty;
                }
            }
        }
        // randomly choose cell in our priority list and actually mark it
        if (_priorityList.markCellList.Count > 0)
        {
            int randomCell = Random.Range(0, _priorityList.markCellList.Count);
            _fieldControl.MarkCellInField(_priorityList.markCellList[randomCell]);
        }
    }

    #endregion
}
