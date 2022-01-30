using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : Interactable
{
    [BoxGroup("Pickup")]
    [EnumFlags]
    public PlayerInventory.Items item;

    protected override void Start()
    {
        base.Start();

        //Assign player inventory pickup callback to OnInteract if unset
        if (OnInteract.GetPersistentEventCount() == 0)
        {
            var playerObject = GameObject.FindGameObjectWithTag("Player");
            var inventoryComponent = playerObject.GetComponent<PlayerInventory>();
            OnInteract.AddListener(inventoryComponent.PickupItem);
        }
    }
}
