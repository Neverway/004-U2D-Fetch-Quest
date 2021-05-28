//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ===========================
//
// Purpose: Don't destroy objects on scene change
// Applied to: Any overworld object that should not be destroyed when changing scenes
//
//====================================================================================

using UnityEngine;

public class System_Persistent : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }


    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DestroyPresistentOverworldObjects");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
