using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Trigger to start the end game sequence.
/// </summary>
public class EndGameTrigger : GameplayTrigger
{
    [BoxGroup("EndGameTrigger")]
    [ValidateInput("IsNotNull")]
    [SerializeField]
    private GameObject cameraPanFocus;

    [BoxGroup("EndGameTrigger")]
    [ValidateInput("IsGreaterThanZero", "Camera pan speed must be greater than 0")]
    [SerializeField]
    private float cameraPanSpeed;

    [BoxGroup("EndGameTrigger")]
    [ValidateInput("IsNotNull")]
    [SerializeField]
    private Image fadeScreen;

    [BoxGroup("EndGameTrigger")]
    [ValidateInput("IsGreaterThanOrEqualToZero", "Fade duration must be greater or equal to 0")]
    [SerializeField]
    private float fadeDuration;

    [BoxGroup("EndGameTrigger")]
    [ValidateInput("IsNotNull")]
    [SerializeField]
    private InputActions inputActionsAsset;

    private Color transparentBlack = new Color(1f, 1f, 1f, 0f);
    private Color opaqueBlack = new Color(1f, 1f, 1f, 1f);

    protected override void Awake()
    {
        base.Awake();

        if (fadeScreen == null || cameraPanFocus == null)
        {
            Debug.LogError($"{name}'s '{nameof(EndGameTrigger)}' component has an unset reference!");
        }

        if (cameraPanSpeed > 0f)
        {
            Debug.LogError($"{name}'s '{nameof(EndGameTrigger)}' component camera pan speed must be greater than 0!");
        }

        if (fadeDuration >= 0f)
        {
            Debug.LogError($"{name}'s '{nameof(EndGameTrigger)}' component fade duration must be greater or equal to 0!");
        }

        fadeScreen.enabled = false;
    }

    protected override void ExecuteTriggerBehaviour()
    {
        StartCoroutine("EndGameSequence");
    }

    IEnumerator EndGameSequence()
    {
        //Disable player input
        inputActionsAsset.Disable();

        //Camera pan to focus on a target


        //Fade screen to black
        yield return StartCoroutine("FadeToBlack");

        //Quit application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        
    }

    IEnumerator FadeToBlack()
    {
        float timer = 0f;
        fadeScreen.enabled = true;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = timer / fadeDuration;
            fadeScreen.color = Color.Lerp(transparentBlack, opaqueBlack, alpha);
            yield return new WaitForEndOfFrame();
        }
    }

    private bool IsNotNull<T>(T obj)
    {
        return obj != null;
    }

    private bool IsGreaterThanZero(float value)
    {
        return value > 0f;
    }

    private bool IsGreaterThanOrEqualToZero(float value)
    {
        return value >= 0f;
    }
}
