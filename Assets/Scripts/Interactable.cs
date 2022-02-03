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

        //Object Can be interacted with if no item required for interaction.
        if (itemRequiredForInteraction == null)
        {
            CanBeInteracted = true;
        }
        //Object cannot be interacted with if item required is not in inventory.
        else if(itemRequiredForInteraction != null && !collision.gameObject.GetComponent<PlayerInventory>().heldItems.Contains(itemRequiredForInteraction.ItemType))
        {
            //Item Required for interaction is not in the player inventory - replace with UI prompt
            Debug.Log("[Interactable] " + name + ": Item - " + itemRequiredForInteraction.name + " - Not Found in Player Inventory.");
        }
        //Object can be interacted with if the item required is in the inventory.
        else if (itemRequiredForInteraction != null && collision.gameObject.GetComponent<PlayerInventory>().heldItems.Contains(itemRequiredForInteraction.ItemType))
        {
            Debug.Log("Required Item Detected.");
            //Remove item from inventory
            CanBeInteracted = true;
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        CanBeInteracted = false;
    }
}
