using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Globals
{
    #region Constants

    // Field size
    public const int Min_Field_Size = 3;
    public const int Max_Field_Size = 30;

    #endregion

    #region Tags

    public const string MARK_CELL_TAG = "MarkCell";

    #endregion
}

public class GameManager : MonoBehaviour
{
    [Header("Field control")]
    // Prefab field control 
    [SerializeField] private GameObject _fieldControl;
    // Win row quantity
    [SerializeField] private int _winRowQuant;
    // Field size
    [Range(Globals.Min_Field_Size, Globals.Max_Field_Size)]
    [SerializeField] private int _fieldSize;
    // MarkField size
    [SerializeField] private float _markFieldSizeOnScreen;

    [Header("Prefabs for input")]
    // Human input
    [SerializeField] private GameObject _inputUser;
    // AI input
    [SerializeField] private GameObject _inputAI;

    // Start is called before the first frame update
    void Start()
    {
        _fieldControl = SpawnManager.GetInstance().SpawnObject(SpawnManager.PoolType.FieldControl, _fieldControl);
        FieldControlManager fieldControlScript = _fieldControl.GetComponent<FieldControlManager>();
        fieldControlScript.CreateField(MarkType.Cross, _winRowQuant, _fieldSize, _markFieldSizeOnScreen, -4);
        fieldControlScript.SetInputs(SpawnManager.GetInstance().SpawnObject(SpawnManager.PoolType.InputAI, _inputAI),
            SpawnManager.GetInstance().SpawnObject(SpawnManager.PoolType.InputUser, _inputUser));

        GameObject fieldControl2 = SpawnManager.GetInstance().SpawnObject(SpawnManager.PoolType.FieldControl, _fieldControl);
        FieldControlManager fieldControlScript2 = fieldControl2.GetComponent<FieldControlManager>();
        fieldControlScript2.CreateField(MarkType.Circle, _winRowQuant, _fieldSize, _markFieldSizeOnScreen, 4);
        fieldControlScript2.SetInputs(SpawnManager.GetInstance().SpawnObject(SpawnManager.PoolType.InputAI, _inputAI),
            SpawnManager.GetInstance().SpawnObject(SpawnManager.PoolType.InputAI, _inputAI));
    }
}
