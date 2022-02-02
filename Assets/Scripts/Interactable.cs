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
    [SerializeField]
    private bool isFocused = false;

    [BoxGroup("Interactable")]
    public GameObjectEvent OnInteract;

    [BoxGroup("Interactable")]
    [SerializeField] private ItemData itemRequiredForInteraction;

    [EnableIf(EConditionOperator.And, "CanBeInteracted", "isFocused")]
    [Button("Interact")]
    public virtual void Interact()
    {
        if (CanBeInteracted && isFocused)
        {
            OnInteract.Invoke(gameObject);
        }
        else
        {
            Debug.LogWarning("Interaction was attempted on an invalid interaction state!");
        }
    }

    public void SetFocused(bool toValue)
    {
        isFocused = toValue;
        SetObjectsActive(toValue);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (itemRequiredForInteraction == null) //[BRIAN NOTE] To do: Add in check to player inventory for required item.
        {
            CanBeInteracted = true;
        }
        else if (collision.gameObject.GetComponent<PlayerInventory>().heldItems.Contains(itemRequiredForInteraction.ItemType)) // [BRIAN NOTE] WIP
        {
            Debug.Log("Required Item Detected.");
            CanBeInteracted = true;
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        CanBeInteracted = false;
    }
}
