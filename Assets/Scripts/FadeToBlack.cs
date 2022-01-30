using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [BoxGroup("EndGameTrigger")]
    [SerializeField]
    private Image fadeScreen;

    [BoxGroup("EndGameTrigger")]
    [ValidateInput("IsGreaterThanOrEqualToZero", "Fade duration must be greater or equal to 0")]
    [SerializeField]
    private float fadeDuration;

    private Color transparentBlack = new Color(0f, 0f, 0f, 0f);
    private Color opaqueBlack = new Color(0f, 0f, 0f, 1f);

    void Awake()
    {
        fadeScreen.enabled = false;
    }

    public IEnumerator Play()
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


    private bool IsGreaterThanOrEqualToZero(float value)
    {
        return value >= 0f;
    }
}
