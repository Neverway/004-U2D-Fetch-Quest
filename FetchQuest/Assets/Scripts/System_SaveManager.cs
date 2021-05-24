//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Manage saving and loading a profile's data
// Applied to: The Config object in a scene
//
//=============================================================================

using System.IO;
using System.Collections;
using System.Xml.Serialization;
using UnityEngine;


public class System_SaveManager : MonoBehaviour
{
    public QuestLogData activeSave;
    [Header ("Runtime Variables")]
    public bool hasLoaded;
    public GameObject[] questLists;
    public GameObject[] quests;
    public Button_Instantiate newQuestList;
    public Button_Instantiate newQuest;


    public void Start()
    {
        Load();
    }


    public void Update()
    {
        questLists = GameObject.FindGameObjectsWithTag("QuestList");
        quests = GameObject.FindGameObjectsWithTag("Quest");
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PreSaveData();
        }
    }


    IEnumerator SaveDelay()
    {
        yield return new WaitForSeconds(0.4f);
        Save();
    }


    IEnumerator FinishPreSaveData()
    {
        yield return new WaitForSeconds(0.1f);
        if (questLists.Length > 0)
        {
            for (int i = 0; i < questLists.Length; i++)
            {
                activeSave.listPosition[i] = i;
                activeSave.listName[i] = questLists[i].GetComponent<QuestList_Data>().questlistName.text; // Save the quest list names
                activeSave.questInlist[i] = questLists[i].GetComponent<QuestList_Data>().questsInList; // Save the number of quests in a quest list
            }
        }

        if (quests.Length > 0)
        {
            for (int i = 0; i < quests.Length; i++)
            {
                activeSave.questName[i] = quests[i].GetComponent<Quest_Data>().titleObject.text; // Save the quest names
            }
        }
        StartCoroutine("SaveDelay");
    }


    IEnumerator FinishPreLoadData()
    {
        yield return new WaitForSeconds(0.1f);

        if (activeSave.listName.Length > 0)
        {
            for (int i = 0; i < activeSave.listName.Length; i++)
            {
                questLists[i].GetComponent<QuestList_Data>().bufferedName = activeSave.listName[i]; // Load the quest list names
                questLists[i].GetComponent<QuestList_Data>().questsInList = activeSave.questInlist[i]; // Load the number of quests in a quest list
            }
        }
    }


    IEnumerator FinishPreLoadData2()
    {
        yield return new WaitForSeconds(0.4f);
        quests = GameObject.FindGameObjectsWithTag("Quest");
        print("ql:" + quests.Length);
        System.Array.Resize(ref activeSave.questName, quests.Length);

        if (quests.Length > 0)
        {
            for (int i = 0; i < activeSave.questName.Length; i++)
            {
                quests[i].GetComponent<Quest_Data>().bufferedName = activeSave.questName[i]; // Load the quest list names
            }
        }
    }


    public void PreSaveData()
    {
        questLists = GameObject.FindGameObjectsWithTag("QuestList");
        quests = GameObject.FindGameObjectsWithTag("Quest");
        System.Array.Resize(ref activeSave.listName, questLists.Length);
        System.Array.Resize(ref activeSave.listPosition, questLists.Length);
        System.Array.Resize(ref activeSave.questInlist, questLists.Length);

        System.Array.Resize(ref activeSave.questName, quests.Length);
        StartCoroutine("FinishPreSaveData");
    }


    public void PreLoadData()
    {
        for (int i = 0; i < activeSave.listName.Length; i++)
        {
            newQuestList.InstantiateObject();
        }
        questLists = GameObject.FindGameObjectsWithTag("QuestList");
        System.Array.Resize(ref activeSave.listName, questLists.Length);
        System.Array.Resize(ref activeSave.listPosition, questLists.Length);
        System.Array.Resize(ref activeSave.questInlist, questLists.Length);
        StartCoroutine("FinishPreLoadData");
        StartCoroutine("FinishPreLoadData2");
    }


    // Save the game data to the current save profile
    public void Save()
    {
        string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(QuestLogData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveProfileName + ".fqsp", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();
        Debug.Log("[ID004 FQ]: " + "Saved information to .fqsp");
    }


    // Load the game data to the current save profile
    public void Load()
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveProfileName + ".fqsp"))
        {
            var serializer = new XmlSerializer(typeof(QuestLogData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveProfileName + ".fqsp", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as QuestLogData;
            stream.Close();
            Debug.Log("[ID004 FQ]: " + "Loaded information from .fqsp");
            PreLoadData();
            hasLoaded = true;
        }
    }


    public void DeleteSaveProfile()
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveProfileName + ".fqsp"))
        {
            File.Delete(dataPath + "/" + activeSave.saveProfileName + ".fqsp");
            Debug.Log("[ID004 FQ]: " + "Deleted current .fqsp");
        }
    }

    [System.Serializable]
    public class QuestLogData
    {
        public string saveProfileName;
        [Header ("Lists")]
        public string[] listName; // The title of the list
        public int[] listPosition; // The position of the list in the menu
        public int[] questInlist; // The position of the list in the menu
        [Header("Quests")]
        public string[] questName; // The title of the list
        //public QuestData[] questData;
    }

    //[System.Serializable]
    //public class QuestData
    //{
    //    public int[] parentListPosition; // The lists position that the quest is from
    //    public string questName; // the title of the quest
    //    public string questDueDate; // The date when the quest should be completed by
    //    public string questDueTime; // The time when the quest should be completed by
    //    public int questPriority; // The priority label of the quest
    //    public string questDescription; // The details about the quest
    //}
}
