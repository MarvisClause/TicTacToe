using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For input check by user and ai
public abstract class InputCheck : MonoBehaviour
{
    // Mark type of the 
    protected MarkType _userMarkType;
    // field, where user must work
    protected FieldControlManager _fieldControl;

    public FieldControlManager FieldControl { get { return _fieldControl; } }

    public void OnEnable()
    {
        _userMarkType = MarkType.Empty;
        _fieldControl = null;
    }

    public virtual void SetInputToField(MarkType inputMarkType, FieldControlManager fieldControl)
    {
        _userMarkType = inputMarkType;
        _fieldControl = fieldControl;
    }
}
