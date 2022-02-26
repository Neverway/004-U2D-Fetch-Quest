using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class System_SavePasser : MonoBehaviour
{
    [Header("1 - QuestBoards, 2 - Quests"), Range(1, 2)]
    public int type;
    public Button_Instantiate buttonPassthrough;
    public UnityEvent save;
    public UnityEvent doneSaving;

    private System_SaveManager saveManager;
/*
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

    IEnumerator SaveDelay()
    {
        yield return new WaitForSeconds(0.4f);
        doneSaving.Invoke();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Save();
        }
    }

    public void Save()
    {
        saveManager.PreSaveData();
        save.Invoke();
        StartCoroutine("SaveDelay");
    }

    public void Load()
    {
        saveManager.Load();
    }

    public void Delete()
    {
        saveManager.DeleteSaveProfile();
    }*/
}
