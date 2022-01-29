using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using NaughtyAttributes;

[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject>
{
}

/// <summary>
/// Interactable activates objects associated with it (UI prompts) 
/// and enables input to be used to do a given action: pick up item, open door, etc.
/// </summary>
public class Interactable : ObjectActivator
{
    [BoxGroup("Interactable")]
    public bool CanBeInteracted = false;
    [BoxGroup("Interactable")]
    public GameObjectEvent OnInteract;

    [EnableIf("CanBeInteracted")]
    [Button("Interact")]
    public virtual void Interact()
    {
        if (CanBeInteracted)
        {
            OnInteract.Invoke(gameObject);
        }
    }

    protected virtual void Update()
    {
        //TODO
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        CanBeInteracted = true;
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        CanBeInteracted = false;
    }
}
