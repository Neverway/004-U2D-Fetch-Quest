//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Manage saving and loading a profile's data
// Applied to: The Config object in a scene
//
//=============================================================================

using System;
using System.IO;
using System.Collections;
using System.Xml.Serialization;
using UnityEngine;


public class System_SaveManager : MonoBehaviour
{
    [Header ("Buffer")]
    public Board boardFile;

    [Header ("Sub-Buffer")]
    public string[] FQSPs; 

    [Header ("Dev Test Controls")]
    public bool save;
    public bool load;
        //System.IO.Directory.GetFiles(dataPath, "*.fqsp");


    private void Update()
    {
        if (save)
        {
            SaveBoard();
            save = false;
        }
        if (load)
        {
            LoadBoard();
            load = false;
        }
    }


    // Save the current buffer as a file
    public void SaveBoard()
    {
        string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(Board));
        var stream = new FileStream(dataPath + "/" + boardFile.boardName + ".fqsp", FileMode.Create);
        serializer.Serialize(stream, boardFile);
        stream.Close();
    }
    

    // Load a boards file data into the buffer
    public void LoadBoard()
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + boardFile.boardName + ".fqsp"))
        {
            var serializer = new XmlSerializer(typeof(Board));
            var stream = new FileStream(dataPath + "/" + boardFile.boardName + ".fqsp", FileMode.Open);
            boardFile = serializer.Deserialize(stream) as Board;
            stream.Close();
        }
    }
    
    
    public void DeleteBoard()
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + boardFile.boardName + ".fqsp"))
        {
            File.Delete(dataPath + "/" + boardFile.boardName + ".fqsp");
        }
    }


    // Reset the buffer to it's default values
    public void ClearBuffer()
    {
            boardFile.boardName = "";
            boardFile.boardColor = new Color (0,0,0,0);
            boardFile.boardPosition = 0;
            Array.Resize(ref boardFile.boardSections, 0);
            Array.Resize(ref boardFile.boardQuests, 0);
            Array.Resize(ref FQSPs, 0);

    }


    public void InstantiateBoards()
    {
    }


    [System.Serializable]
    public class Board
    {
        public string boardName; // The title of the project or main group (example: Chores)
        public Color boardColor; // The color of the board on the board selection screen
        public int boardPosition; // The order in which the board appears on the board selection screen (starting from least to greatest)

        public Section[] boardSections;
        public Quest[] boardQuests;
    }
    
    [System.Serializable]
    public class Section
    {
        public string sectionName; // The title of the section (example: School)
        public int sectionPosition; // The order in which the section appears on the board screen (starting from least to greatest)
    }
    
    [System.Serializable]
    public class Quest
    {
        public string questName; // The title of the task (example: Finish homework)
        public int sectionPosition; // The subsection pannel that the task appears under (starting from left to right) basicaly the quests parent category
        public int questPosition; // The order in which the quests appear under a subsection (starting from least to greatest)
    }
}