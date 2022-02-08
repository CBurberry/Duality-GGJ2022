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
    [SerializeField] public List<ItemData> itemRequiredForInteraction;

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

        //Object Can be interacted with if no items are required for interaction.
        if (itemRequiredForInteraction.Count <= 0)
        {
            Debug.Log("[INTERACTABLE] - No items found in ItemsRequiredForInteraction List");
            CanBeInteracted = true;
        }
        //Object checked for items to be required are present
        else if(itemRequiredForInteraction.Count > 0)
        {
            PlayerInventory playerInventory = collision.gameObject.GetComponent<PlayerInventory>();

            foreach (ItemData item in itemRequiredForInteraction)
            {
                //Object can be interacted with if the object is found (will continue check for other items).
                if (playerInventory.heldItems.Contains(item.ItemType))
                {
                    Debug.Log("[INTERACTABLE] - " + item.ItemType + " found.");
                    CanBeInteracted = true;
                }
                //Object cannot be interacted with if an item required is not in inventory.
                else
                {
                    Debug.Log("[INTERACTABLE] - " + item.ItemType + " NOT FOUND.");
                    CanBeInteracted = false;
                    return;
                }
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        CanBeInteracted = false;
    }
}
