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
    [Header("Runtime Variables")]
    public bool hasLoaded;
    public GameObject[] questLists;
    public GameObject[] quests;
    public GameObject[] questBoards;
    public Button_Instantiate newQuestList;
    public Button_Instantiate newQuest;
    public Button_Instantiate newEntry;
    public string[] saveFiles;
    public int questNameID;


    public void Awake()
    {
        questNameID = 0;
    }


    public void Update()
    {
    }


    IEnumerator SaveDelay()
    {
        yield return new WaitForSeconds(0.4f);
        questNameID = 0;
        Save();
    }


    IEnumerator FinishPreSaveData()
    {
        yield return new WaitForSeconds(0.1f);
        if (questLists.Length > 0)
        {
            for (int i = 0; i < questLists.Length; i++)
            {
                activeSave.questListData[i].listName = questLists[i].GetComponent<QuestList_Data>().questlistName.text; // Save the quest list names
                System.Array.Resize(ref activeSave.questListData[i].questData, questLists[i].GetComponent<QuestList_Data>().questsInList);

                for (int ii = 0; ii < activeSave.questListData[i].questData.Length; ii++)
                {
                    activeSave.questListData[i].questData[ii].questName = quests[questNameID].GetComponent<Quest_Data>().titleObject.text; // Save the quest list names
                    questNameID++;
                }
            }
        }

        if (quests.Length > 0)
        {

        }
        StartCoroutine("SaveDelay");
    }

    IEnumerator FinishQuestBoards()
    {
        yield return new WaitForSeconds(0.1f);

        if (activeSave.boardName.Length > 0)
        {
            for (int i = 0; i < activeSave.boardName.Length; i++)
            {
                string dataPath = Application.persistentDataPath;
                activeSave.boardName[i] = saveFiles[i].Replace(dataPath + "/", "");
                activeSave.boardName[i] = activeSave.boardName[i].Replace(".fqsp", "");
                questBoards[i].GetComponent<QuestBoard_Data>().bufferedName = activeSave.boardName[i]; // Load the quest list names
            }
        }
    }

    IEnumerator FinishPreLoadData()
    {
        yield return new WaitForSeconds(0.1f);

        if (activeSave.questListData.Length > 0)
        {
            for (int i = 0; i < activeSave.questListData.Length; i++)
            {
                questLists[i].GetComponent<QuestList_Data>().bufferedName = activeSave.questListData[i].listName; // Load the quest list names
                //questLists[i].GetComponent<QuestList_Data>().questsInList = ;// !!!!!!!!!!!!!!!!!!!!!
                //System.Array.Resize(ref activeSave.questListData[i].questData, questLists[i].GetComponent<QuestList_Data>().questsInList);
                for (int ii = 0; ii < activeSave.questListData[i].questData.Length; ii++)
                {
                    questLists[i].GetComponent<QuestList_Data>().newQuestButton.InstantiateObject();
                    questLists[i].GetComponent<QuestList_Data>().questsInListBuffered += 1;
                }
            }
        }
    }


    IEnumerator FinishPreLoadData2()
    {
        yield return new WaitForSeconds(0.4f);
        quests = GameObject.FindGameObjectsWithTag("Quest");
        if (activeSave.questListData.Length > 0)
        {
            for (int i = 0; i < activeSave.questListData.Length; i++)
            {
                //for (int ii = 0; ii < activeSave.questListData[i].questData.Length; ii++)
                //{
                //    quests[ii].GetComponent<Quest_Data>().bufferedName = activeSave.questListData[i].questData[ii].questName; // Load the quest names
                //}

                for (int ii = 0; ii < activeSave.questListData[i].questData.Length; ii++)
                {
                    quests[questNameID].GetComponent<Quest_Data>().bufferedName = activeSave.questListData[i].questData[ii].questName; // Load the quest list names
                    questNameID++;
                }
            }
        }
        questNameID = 0;
    }

    public void findSaveFiles()
    {
        string dataPath = Application.persistentDataPath;
        saveFiles = System.IO.Directory.GetFiles(dataPath, "*.fqsp");

        for (int i = 0; i < saveFiles.Length; i++)
        {
            newEntry.InstantiateObject();
        }
        questBoards = GameObject.FindGameObjectsWithTag("QuestBoard");
        System.Array.Resize(ref activeSave.boardName, questBoards.Length);
        StartCoroutine("FinishQuestBoards");
    }

    public void PreSaveData()
    {
        questNameID = 0;
        questLists = GameObject.FindGameObjectsWithTag("QuestList");
        quests = GameObject.FindGameObjectsWithTag("Quest");
        System.Array.Resize(ref activeSave.questListData, questLists.Length);
        StartCoroutine("FinishPreSaveData");
    }


    public void PreLoadData()
    {
        for (int i = 0; i < activeSave.questListData.Length; i++)
        {
            newQuestList.InstantiateObject();
        }
        questLists = GameObject.FindGameObjectsWithTag("QuestList");
        quests = GameObject.FindGameObjectsWithTag("Quest");
        //System.Array.Resize(ref activeSave.listName, questLists.Length);
        //System.Array.Resize(ref activeSave.listPosition, questLists.Length);
        //System.Array.Resize(ref activeSave.questInlist, questLists.Length);
        StartCoroutine("FinishPreLoadData");
        StartCoroutine("FinishPreLoadData2");
    }


    // Save the game data to the current save profile
    public void Save()
    {
        questNameID = 0;
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
        [Header("Boards")]
        public string[] boardName; // The title of the list
        [Header("Lists")]
        //public string[] listName; // The title of the list
        //public int[] listPosition; // The position of the list in the menu
        //public int[] questInlist; // The position of the list in the menu
        //[Header("Quests")]
        //public string[] questName; // The title of the list
        public QuestListData[] questListData;
    }

    [System.Serializable]
    public class QuestListData
    {
        public string listName; // The title of the list
        //public int listPosition; // The position of the list in the menu
        public QuestData[] questData;
    }

    [System.Serializable]
    public class QuestData
    {
        public string questName; // The title of the list
        //public int questPosition; // The position of the list in the menu
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
