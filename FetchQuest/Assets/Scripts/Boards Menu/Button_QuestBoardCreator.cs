using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_QuestBoardCreator : MonoBehaviour
{
    public InputField inputField;
    public Button button;
    public bool instantiateBoards;
    public bool twoactive;
    public bool twowactive;

    public UI_ParentHandling[] boards;
    private bool refreshing;

    private System_SaveManager saveManager;
    private string dataPath;

    void Start()
    {
        saveManager = FindObjectOfType<System_SaveManager>();
        dataPath = Application.persistentDataPath;
        three();
    }
    
    IEnumerator waitForBuffer()
    {
        yield return new WaitForSeconds(0.2f);
        LoadQuestBoards();
        refreshing = false;
    }

    void Update()
    {
        if (instantiateBoards)
        {
            LoadQuestBoards();
            instantiateBoards = false;
        }
        if (twoactive)
        {
            two();
            twoactive = false;
        }
        if (twowactive)
        {
            three();
            twowactive = false;
        }
        if(inputField.text != "")
        {
            if(!inputField.text.Contains("/") && !inputField.text.Contains("."))
            {
                if (!System.IO.File.Exists(dataPath + "/" + inputField.text + ".fqsp"))
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
            }
            else
            {
                button.interactable = false;
            }
        }
        else if (inputField.text == "") 
        {
            button.interactable = false;
        }

    }

    public void CreateQuestBoard()
    {
        string dataPath = Application.persistentDataPath;
        saveManager.FQSPs = System.IO.Directory.GetFiles(dataPath, "*.fqsp");
        saveManager.boardFile.boardName = inputField.text;
        saveManager.boardFile.boardPosition = saveManager.FQSPs.Length;
        saveManager.SaveBoard();
        saveManager.ClearBuffer();
        three();
    }

    public void LoadQuestBoards()
    {
        string dataPath = Application.persistentDataPath;
        saveManager.FQSPs = System.IO.Directory.GetFiles(dataPath, "*.fqsp");
        foreach (string dir in saveManager.FQSPs){print(dir);}

        // Reset

        // Create boards
        for (int i = 0; i < saveManager.FQSPs.Length; i++)
        {

            // Remove junk from FQSPs
            saveManager.FQSPs[i] = saveManager.FQSPs[i].Replace((dataPath + "/"), "");
            saveManager.FQSPs[i] = saveManager.FQSPs[i].Replace(".fqsp", "");


            gameObject.GetComponent<Button_Instantiate2>().InstantiateObject();
            boards = FindObjectsOfType<UI_ParentHandling>();
        }
        two();
    }

    public void two()
    {
        // Assign board names
        for (int i = 0; i < boards.Length; i++)
        {
            
            for (int ii = 0; ii < saveManager.FQSPs.Length; ii++)
            {
                saveManager.boardFile.boardName = saveManager.FQSPs[ii];
                saveManager.LoadBoard();
                if (boards[i].position == saveManager.boardFile.boardPosition)
                {
                    boards[i].gameObject.GetComponent<UI_BoardDataSetter>().name = saveManager.boardFile.boardName;
                    print ("Matched!");
                }
            }
            // print (saveManager.FQSPs[i]);
            // //saveManager.boardFile.boardName = saveManager.FQSPs[i];
            // print("BoardPos: " + boards[i].position + " | FileBp: " + saveManager.boardFile.boardPosition);
            // saveManager.LoadBoard();
            // if (boards[i].position == saveManager.boardFile.boardPosition)
            // {
            //     boards[i].gameObject.GetComponent<UI_BoardDataSetter>().name = saveManager.boardFile.boardName;
            //     print ("Matched!");
            // }
        }
    }

    public void three()
    {
        if (!refreshing)
        {
            refreshing = true;
            for (int i = 0; i < boards.Length; i++)
            {
                Destroy(boards[i].gameObject);
            }
            Array.Resize(ref boards, 0);
            StartCoroutine(waitForBuffer());
        }
    }
}
