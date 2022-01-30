using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField] AudioSource footstepsAudioSource;

    [Header("Audio Clips")]
    [SerializeField] AudioClip[] steps;

    [Header("Steps")]
    [SerializeField] float stepsTimeGap = 1f;

    private float stepsTimer;

    public void PlaySteps()
    {
        stepsTimer += Time.fixedDeltaTime * 1;

        if (stepsTimer >= stepsTimeGap)
        {
            int index = Random.Range(0, steps.Length);
            footstepsAudioSource.PlayOneShot(steps[index]);

            stepsTimer = 0;
        }
    }

}
