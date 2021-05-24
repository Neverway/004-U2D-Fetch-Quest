//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ===========================
//
// Purpose: Destroy a object when a button is pressed
// Applied to: Any canvas button object in a saved scene
//
//====================================================================================

using UnityEngine;

public class Button_Destroy : MonoBehaviour
{
    public GameObject deletionTarget;             // The object to destroy when the button is pressed


    public void DestroyObject()
    {
        Destroy(deletionTarget);    // Destroy the target
    }
}
