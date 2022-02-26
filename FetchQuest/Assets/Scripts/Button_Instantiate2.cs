//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// SID: 
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Instantiate2 : MonoBehaviour
{
    // Public variables
    public GameObject UIPrefab;
    public GameObject parent;

    // Private variables
    private UI_ParentHandling[] instantiatedObjects;

    // Reference variables


    public void InstantiateObject()
    {
        Instantiate(UIPrefab);
        instantiatedObjects = FindObjectsOfType<UI_ParentHandling>();

        for (int i = 0; i < instantiatedObjects.Length; i++)
        {
            if (!instantiatedObjects[i].properlyParented)
            {
                Debug.Log("Found unparented object! Parenting...");
                instantiatedObjects[i].position = parent.transform.childCount;
                instantiatedObjects[i].gameObject.transform.SetParent(parent.transform, false);
                instantiatedObjects[i].properlyParented = true;
            }
        }
    }
}
