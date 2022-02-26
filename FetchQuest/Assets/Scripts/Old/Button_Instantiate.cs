//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ===========================
//
// Purpose: Create a new menu object when a button is pressed
// Applied to: Any canvas button object in a saved scene
//
//====================================================================================

using UnityEngine;

public class Button_Instantiate : MonoBehaviour
{
    public GameObject UIPrefab;             // The prefab to spawn when the button is pressed
    public GameObject parent;               // The object that the new prefab should be parented to
    [Header ("1 - QuestList, 2 - Quest, 3 - QuestBoard"), Range (1,3)]
    public int type;                        // Which menu object is it trying to spawn
    private GameObject[] SimilarPrefabs;    // Find similar prefabs so they can be properly parented


    public void InstantiateObject()
    {
        // Spawn the prefab
        Instantiate(UIPrefab);

        // Find unparented prefabs
        if (type == 1)
        {
            SimilarPrefabs = GameObject.FindGameObjectsWithTag("QuestList");
        }
        else if (type == 2)
        {
            SimilarPrefabs = GameObject.FindGameObjectsWithTag("Quest");
        }
        else if (type == 3)
        {
            SimilarPrefabs = GameObject.FindGameObjectsWithTag("QuestBoard");
        }

        // Properly parent the unparented prefabs
        foreach (var obj in SimilarPrefabs)
        {
            if (!obj.GetComponent<System_ListState>().properlyParented)
            {
                obj.transform.SetParent(parent.transform, false);               // Set the prefabs parent
                obj.GetComponent<System_ListState>().properlyParented = true;   // Label the prefab as being properly parented
                obj.GetComponent<System_ListState>().parent = parent;           // Store the prefabs parent
            }
        }
    }
}
