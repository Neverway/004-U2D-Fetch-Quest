using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList_DataPass : MonoBehaviour
{
    public QuestList_Data questListData;

    public void RemoveQuest()
    {
        questListData.RemoveQuest();
    }
}
