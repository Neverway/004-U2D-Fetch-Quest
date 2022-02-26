using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class System_ShowCurrentSaveProfile : MonoBehaviour
{
    public Text targetText;
    private System_SaveManager saveManager;
    /*
    void Start()
    {
        saveManager = FindObjectOfType<System_SaveManager>();
        targetText.text = saveManager.activeSave.saveProfileName;
    }*/
}
