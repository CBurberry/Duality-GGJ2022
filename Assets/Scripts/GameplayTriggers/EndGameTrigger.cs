using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Trigger to start the end game sequence.
/// </summary>
public class EndGameTrigger : GameplayTrigger
{
    /*[BoxGroup("EndGameTrigger")]
    [SerializeField]
    private GameObject cameraPanFocus;

    [BoxGroup("EndGameTrigger")]
    [ValidateInput("IsGreaterThanZero", "Camera pan speed must be greater than 0")]
    [SerializeField]
    private float cameraPanSpeed;*/

    [BoxGroup("EndGameTrigger")]
    [SerializeField]
    private InputActionAsset inputActionsAsset;

    protected override void ExecuteTriggerBehaviour()
    {
        StartCoroutine("EndGameSequence");
    }

    IEnumerator EndGameSequence()
    {
        Debug.Log("End Game Sequence Started!");

        //Disable player input - BUGGED
        inputActionsAsset.Disable();

        //TODO - Camera pan to focus on a target

        //Fade screen to black
        var fadeComponent = GetComponent<FadeToBlack>();
        yield return StartCoroutine(fadeComponent.Play());

        //Quit application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif 
    }

    private bool IsGreaterThanZero(float value)
    {
        return value > 0f;
    }
}
