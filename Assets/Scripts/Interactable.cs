using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Interactable activates objects associated with it (UI prompts) 
/// and enables input to be used to do a given action: pick up item, open door, etc.
/// </summary>
public class Interactable : ObjectActivator
{
    public bool CanBeInteracted = false;

    public virtual void Interact()
    {
        //TODO
    }

    protected virtual void Update()
    {
        if (CanBeInteracted)
        {
            var gamepad = Gamepad.current;
        }
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
