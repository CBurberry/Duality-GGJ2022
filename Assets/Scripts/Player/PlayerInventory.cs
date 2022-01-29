using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Player component class that defines possible items that can be picked up 
/// & manages the associated UI.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    public enum Items
    {
        Key,
        Saucepan_Empty,
        Saucepan_WaterFilled,
        Tea,
        Cup_Empty,
        Cup_TeaFilled,
        BloodiedKnife
    }

    public List<Items> heldItems;

    //All data classes to reference for any held items.
    [SerializeField]
    private List<ItemData> itemData;

    //UI element associated for displaying items
    [SerializeField]
    private HorizontalOrVerticalLayoutGroup layoutGroup;

    private string playerInventoryAssetName = "PlayerInventoryUI";

    void Awake()
    {
        var inventoryObj = GameObject.Find(playerInventoryAssetName);
        if (inventoryObj != null)
        {
            layoutGroup = inventoryObj.GetComponent<HorizontalOrVerticalLayoutGroup>();
        }

        if (layoutGroup == null)
        {
            Debug.LogError($"{GetType().Name} could not find a '{playerInventoryAssetName}' UI element in Scene!");
        }
    }

    public void PickupItem(GameObject itemObject)
    {
        Pickup pickup = itemObject.GetComponent<Pickup>();
        if (pickup == null)
        {
            Debug.LogError($"{GetType().Name} picked up an item '{itemObject.name}' which did not have an associated '{nameof(Pickup)}' component.");
            return;
        }

        Debug.Log($"Picked up an item of type '{pickup.item.ToString()}'!");
        AddItem(pickup.item);
    }

    private void AddItem(Items item)
    {
        heldItems.Add(item);

        //TODO - update UI elements
    }

    private void ReplaceItem(Items itemToRemove, Items itemToAdd)
    {
        heldItems.Add(itemToAdd);
        heldItems.Remove(itemToRemove);

        //TODO - update UI elements
    }

    private void RemoveItem(Items item)
    {
        heldItems.Remove(item);

        //TODO - update UI elements
    }

    private ItemData GetItemData(Items item)
    {
        return itemData.First(x => x.ItemType == item);
    }
}
