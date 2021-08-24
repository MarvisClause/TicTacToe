using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text[] buttonList;

    public void Awake()
    {
        SetGameManagerReferenceOnButtons();
    }
    void SetGameManagerReferenceOnButtons()
    {
        for(int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameManagerReference(this);
        }
    } 

    public string GetPlayerSide()
    {
        return "&";
    } 
    public void EndTurn()
    {
        Debug.Log("EndTurn is not implemented!");
    }
}
