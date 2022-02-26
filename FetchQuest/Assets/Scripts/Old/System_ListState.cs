//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ===========================
//
// Purpose: Keep track of the state of each questlist
// Applied to: The root of a questlist prefab
//
//====================================================================================

using UnityEngine;

public class System_ListState : MonoBehaviour
{
    public bool properlyParented;   // Is the questlist in the proper place in the heirarchy
    public GameObject parent;
}