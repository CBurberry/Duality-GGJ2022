using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGameTrigger : GameplayTrigger
{
    [SerializeField] AudioSource _audioSource;

    [Header("Audio Clips")]
    [SerializeField] AudioClip[] sfx;


    protected override void ExecuteTriggerBehaviour()
    {
        int index = Random.Range(0, sfx.Length);
        _audioSource.PlayOneShot(sfx[index]);
    }

}
