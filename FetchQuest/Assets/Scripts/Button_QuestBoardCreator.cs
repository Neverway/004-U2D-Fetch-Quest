using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_QuestBoardCreator : MonoBehaviour
{
    public InputField inputField;
    public Button button;
    private System_SaveManager saveManager;
    private string dataPath;

    void Start()
    {
        saveManager = FindObjectOfType<System_SaveManager>();
        dataPath = Application.persistentDataPath;
    }

    void Update()
    {
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
        saveManager.activeSave.saveProfileName = inputField.text;
    }
}
