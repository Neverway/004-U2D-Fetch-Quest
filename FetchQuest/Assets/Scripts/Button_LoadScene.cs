//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ===========================
//
// Purpose: Load a scene when a button is pressed
// Applied to: Any canvas button object in a scene
//
//====================================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_LoadScene : MonoBehaviour
{
    private string bufferedScene;
    private float bufferedDelay = 1.2f;

    IEnumerator changeSceneDelayed()
    {
        yield return new WaitForSeconds(bufferedDelay);
        SceneManager.LoadScene(bufferedScene);
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void DelayedChangeScene(string scene)
    {
        bufferedScene = scene;
        StartCoroutine("changeSceneDelayed");
    }
}
