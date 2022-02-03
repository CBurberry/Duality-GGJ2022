using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class HideBehind : Interactable
{
    protected override void Start()
    {
        base.Start();

        var playerObject = GameObject.FindGameObjectWithTag("Player");
        var playerInteractionComponent = playerObject.GetComponent<PlayerInteraction>();
        OnInteract.AddListener(playerInteractionComponent.HideUnHideCharacter);
    }
}
