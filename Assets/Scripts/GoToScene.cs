using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

/// <summary>
/// Simple class that exposes a public function to change the current scene to another.
/// </summary>
public class GoToScene : MonoBehaviour
{
    [Scene]
    public string TargetScene;

    public void GoToTargetScene()
    {
        if (TargetScene == SceneManager.GetActiveScene().name)
        {
            Debug.LogWarning($"'{gameObject.name}' attempted a scene transition to the currently loaded scene! Aborting.");
            return;
        }

        SceneManager.LoadScene(TargetScene);
    }
}
