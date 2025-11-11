using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// DESC: scene manager manages scenes el inch asem

public class ScenesManager : MonoBehaviour
{

    public void LoadSomeScene(int sceneNumber)
    {
        SceneManager.LoadSceneAsync(sceneNumber, LoadSceneMode.Single);
    }
    
    public void UnloadAndAdd(int i, int j) // can be used when colliding with teleport box, you'd want to remove the previous scene you stepped out of and add the next.
    {
        if (SceneManager.GetActiveScene() == null)
        {
            Debug.Log("SCENEMANAGER: failed to unload scene");
            return;
        }
        UnloadScene(i);
        AddSomeScene(j);
    }

    public void AddSomeScene(int i)
    {
        SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
    }

    public void UnloadScene(int i)
    {
        if (SceneManager.GetSceneByBuildIndex(i) == null) Debug.Log(" you cannot remove this scene because it doesn't exist rn.");
        SceneManager.UnloadSceneAsync(i);
    }
}
