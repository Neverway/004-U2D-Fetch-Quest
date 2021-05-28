using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OpenInFileBrowser : MonoBehaviour
{
    public void ImportFQSPFile()
    {
        string dataPath = Application.persistentDataPath;
        EditorUtility.OpenFilePanel("Select a FetchQuest save profile (.fqsp)", dataPath, "");
    }

    public void OpenSaveFolder()
    {
        string dataPath = Application.persistentDataPath;
        System.Diagnostics.Process.Start("open", $"-R \"datapath\"");
    }
}