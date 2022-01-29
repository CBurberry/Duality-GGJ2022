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

        //Get Player object in scene
        var playerObject = GameObject.FindGameObjectWithTag("Player");

        //Get Inventory component
        var inventoryComponent = playerObject.GetComponent<PlayerInventory>();

        //Assign function
        OnInteract.AddListener(inventoryComponent.PickupItem);
    }
}
