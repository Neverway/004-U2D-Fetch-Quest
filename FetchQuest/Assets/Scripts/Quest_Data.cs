//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ===========================
//
// Purpose: To hold the data written to a quest so it can be shown when editing a quest
// Applied to: The root of a quest prefab
//
//====================================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Data : MonoBehaviour
{
    public InputField titleObject;                    // Variable to store and set the title of the current quest
    public string dueDate;                      // Variable to store the due date of the current quest
    public string dueTime;                      // Variable to store the due time of the current quest
    public int priority;                        // Variable to store the priority of the current quest
    public string description;                  // Variable to store the description of the current quest
    private System_QuestOptions questOptions;   // Get a reference to the script that controlls the quest edit menu

    public string bufferedName;
    private System_SaveManager saveManager;     // Get a reference to the save script so the buffered name knows when to load properly
    private bool firstPass = true;


    void Start()
    {
        questOptions = FindObjectOfType<System_QuestOptions>();     // Find the script that controls the quest edit menu in the current scene (There should only be one)
        saveManager = FindObjectOfType<System_SaveManager>();       // Find the save script in the current scene (There should only be one)
    }


    IEnumerator firstPassDelay()
    {
        yield return new WaitForSeconds(1f);
        firstPass = false;
    }


    // When loading a quest file, set the questlists name
    void Update()
    {
        if (saveManager.hasLoaded && firstPass)
        {
            titleObject.text = bufferedName;
            StartCoroutine("firstPassDelay");
        }
    }


    // Pass the quests information to the quest edit menu
    public void PassQuestData()
    {
        questOptions.currentQuestTarget = gameObject;                                   // Set the current quest prefab as the one that is being edited
        questOptions.PassData(titleObject.text, dueDate, dueTime, priority, description);    // Send the quest prefabs data to the quest edit menu
    }
}
