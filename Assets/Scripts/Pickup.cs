using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : Interactable
{
    [BoxGroup("Pickup")]
    public PlayerInventory.Items item;
    [BoxGroup("Pickup")]
    public bool noninteractableAfterPickUp = true;
    [BoxGroup("Pickup")]
    public bool deactivateOnPickUp = true;


    protected override void Start()
    {
        base.Start();

        var playerObject = GameObject.FindGameObjectWithTag("Player");
        var inventoryComponent = playerObject.GetComponent<PlayerInventory>();
        OnInteract.AddListener(inventoryComponent.PickupItem);
    }
}
