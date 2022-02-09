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
    [SerializeField] Animator musicAnim;
    [SerializeField] float transitionTime = 3;

    [Scene]
    public string TargetScene;

    
    IEnumerator ChangeScene(string _aRequestedSceneName)
    {
        musicAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(_aRequestedSceneName);
    }



    public void GoToTargetScene()
    {
        /*if (TargetScene == SceneManager.GetActiveScene().name)
        {
            Debug.LogWarning($"'{gameObject.name}' attempted a scene transition to the currently loaded scene! Aborting.");
            return;
        }*/

        StartCoroutine(ChangeScene(TargetScene));
    }
}
