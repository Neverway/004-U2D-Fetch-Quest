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

public class UI_BoardEditor : MonoBehaviour
{
    // Public variables
    public GameObject UITarget;
    public Text boardTitle;
    public InputField boardName;


    // Private variables

    // Reference variables
    private System_SaveManager saveManager;
    private Button_QuestBoardCreator questBoardManager;


    void Start()
    {
        saveManager = FindObjectOfType<System_SaveManager>();
        questBoardManager = FindObjectOfType<Button_QuestBoardCreator>();
    }


    void Update()
    {
	
    }

    public void EditBoard()
    {
        UITarget.SetActive(true);
        boardTitle.text = saveManager.boardFile.boardName;
        boardName.text = saveManager.boardFile.boardName;
    }

    public void DeleteBoard()
    {
        string dataPath = Application.persistentDataPath;
        saveManager.FQSPs = System.IO.Directory.GetFiles(dataPath, "*.fqsp");
        if (saveManager.boardFile.boardPosition == (saveManager.FQSPs.Length-1))
        {
            saveManager.DeleteBoard();
            questBoardManager.three();
        }
        else
        {
            print("Cycle list!");
            print(saveManager.boardFile.boardPosition + " | " + saveManager.FQSPs.Length);
        }
    }
}
