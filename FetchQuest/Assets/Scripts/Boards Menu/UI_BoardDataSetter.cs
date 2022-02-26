//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// SID: 
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BoardDataSetter : MonoBehaviour
{
    // Public variables
    public string name;
    public Text nameObject;

    // Private variables

    // Reference variables
    private System_SaveManager saveManager;
    private UI_BoardEditor boardEditor;


    void Start()
    {
        saveManager = FindObjectOfType<System_SaveManager>();
        boardEditor = FindObjectOfType<UI_BoardEditor>();
    }


    void Update()
    {
        nameObject.text = name;
    }

    public void EditBoard()
    {
        saveManager.boardFile.boardName = name;
        saveManager.LoadBoard();
        boardEditor.EditBoard();
    }
}
