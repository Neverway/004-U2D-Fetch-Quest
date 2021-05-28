
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBoard_Data : MonoBehaviour
{
    public string bufferedName;
    public Text questBoardTitle;
    private System_SaveManager saveManager;
    // Start is called before the first frame update
    void Start()
    {
        saveManager = FindObjectOfType<System_SaveManager>();
    }

    void Update()
    {
        questBoardTitle.text = bufferedName;
    }

    public void selectCurrentBoard()
    {
        saveManager.activeSave.saveProfileName = bufferedName;
    }
}
