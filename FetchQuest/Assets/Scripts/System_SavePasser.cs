using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_SavePasser : MonoBehaviour
{
    [Header("1 - QuestBoards, 2 - Quests"), Range(1, 3)]
    public int type;
    public Button_Instantiate buttonPassthrough;

    private System_SaveManager saveManager;

    void Start()
    {
        saveManager = FindObjectOfType<System_SaveManager>();
        if (type == 1)
        {
            saveManager.newEntry = buttonPassthrough;
            saveManager.findSaveFiles();
        }

        else if (type == 2)
        {
            saveManager.newQuestList = buttonPassthrough;
            saveManager.Load();
        }
    }
}
