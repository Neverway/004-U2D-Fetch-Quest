//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ===========================
//
// Purpose: To hold the data written to a questlist so it can be saved for later
// Applied to: The root of a questlist prefab
//
//====================================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuestList_Data : MonoBehaviour
{
    public InputField questlistName;            // The UI text object that displays and stores the questlists name
    public int questsInList;                    // The UI text object that displays and stores the questlists name
    public string bufferedName;                 // Store the questlists name when loading the lists so it can be properly called without being imediatly overwritten
    public Button_Instantiate newQuestButton;

    private System_SaveManager saveManager;     // Get a reference to the save script so the buffered name knows when to load properly
    private bool trueFirstPass = true;
    private bool firstPass = true;


    void Start()
    {
        saveManager = FindObjectOfType<System_SaveManager>();   // Find the save script in the current scene (There should only be one)
    }


    IEnumerator trueFirstPassDelay()
    {
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator firstPassDelay()
    {
        yield return new WaitForSeconds(1f);
        trueFirstPass = false;
        firstPass = false;
    }


    // When loading a quest file, set the questlists name
    void Update()
    {
        if (saveManager.hasLoaded && trueFirstPass)
        {
            print("1");
            if (questsInList > 0)
            {
                print("2");
                for (int i = 0; i < questsInList; i++)
                {
                    newQuestButton.InstantiateObject();
                    print("3");
                }
                trueFirstPass = false;
            }
        }
        if (saveManager.hasLoaded && firstPass)
        {
            questlistName.text = bufferedName;
            StartCoroutine("firstPassDelay");
        }
    }

    public void AddQuest()
    {
        questsInList += 1;
    }

    public void RemoveQuest()
    {
        questsInList -= 1;
    }
}
