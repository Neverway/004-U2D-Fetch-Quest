//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ===========================
//
// Purpose: Control the quest edit menu
// Applied to: The root of the quest edit menu
//
//====================================================================================

using UnityEngine;
using UnityEngine.UI;

public class System_QuestOptions : MonoBehaviour
{
    public GameObject questEditMenu;        // Get a reference to the quest edit menu so it can be turned on and off
    public Text titleObject;                // The UI text object that displays and stores the quests name
    public InputField dueDateObject;        // The UI text object that displays and stores the quests due date
    public InputField dueTimeObject;        // The UI text object that displays and stores the quests due time
    public Dropdown priorityObject;         // The UI text object that displays and stores the quests priority
    public InputField descriptionObject;    // The UI text object that displays and stores the quests description
    public GameObject currentQuestTarget;   // Set by the Quest_Data script, this is how the quest edit menu knows wich quest it's editing
    public GameObject parentList;           // Set by the Quest_Data script, this is how the quest edit menu knows wich questlist the quest is a child of


    // A function to send the current quests data to the quest edit menu
    public void PassData(string title, string dueDate, string dueTime, int priority, string description, GameObject parent)
    {
        questEditMenu.SetActive(true);
        titleObject.text = title;
        dueDateObject.text = dueDate;
        dueTimeObject.text = dueTime;
        priorityObject.SetValueWithoutNotify(priority);
        descriptionObject.text = description;
        parentList = parent;
    }


    // Save and apply the edits made to the quest in the quest edit menu
    public void ApplyData()
    {
        //currentQuestTarget.GetComponent<Quest_Data>().titleObject = titleObject.text;                // Send the quest edit menu title to the quest
        currentQuestTarget.GetComponent<Quest_Data>().dueDate = dueDateObject.text;             // Send the quest edit menu due date to the quest
        currentQuestTarget.GetComponent<Quest_Data>().dueTime = dueTimeObject.text;             // Send the quest edit menu due time to the quest
        currentQuestTarget.GetComponent<Quest_Data>().priority = priorityObject.value;          // Send the quest edit menu priority to the quest
        currentQuestTarget.GetComponent<Quest_Data>().description = descriptionObject.text;     // Send the quest edit menu description to the quest
        currentQuestTarget = null;                                                              // Clear the currently editing quest since it's no longer editing a quest
    }

    public void DeleteQuest()
    {
        Destroy(currentQuestTarget);
        parentList.GetComponent<QuestList_DataPass>().RemoveQuest();
    }
}
