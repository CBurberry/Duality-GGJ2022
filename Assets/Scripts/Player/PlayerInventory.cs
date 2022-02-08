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
        Kettle_Empty,
        Kettle_WaterFilled,
        Tea,
        Cup_Empty,
        Cup_TeaFilled,
        Teddy,
        BloodiedKnife
    }

    public List<Items> heldItems;

    //All data classes to reference for any held items.
    [SerializeField]
    private List<ItemData> itemData;

    //UI prefab to instantiate in the layout group
    [SerializeField]
    private GameObject iconPrefab;

    //UI element associated for displaying items
    private GridLayoutGroup layoutGroup;

    //Map of item enum to icon
    private Dictionary<Items, GameObject> itemIconReferences = new Dictionary<Items, GameObject>();

    private string playerInventoryAssetName = "PlayerInventoryUI";

    private List<GameObject> inventoryIcons = new List<GameObject>(); 

    void Awake()
    {
        var inventoryObj = GameObject.Find(playerInventoryAssetName);
        if (inventoryObj != null)
        {
            layoutGroup = inventoryObj.GetComponent<GridLayoutGroup>();
        }

        if (layoutGroup == null)
        {
            Debug.LogError($"{name}'s {GetType().Name} component could not find a '{playerInventoryAssetName}' UI element in Scene!");
        }

        if (iconPrefab == null)
        {
            Debug.LogError($"{name}'s {GetType().Name} component reference to icon UI prefab was not set!");
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

        if(pickup.itemRequiredForInteraction.Count > 0) //[BRIAN NOTE] Removes each item required for Interaction upon use.
        {
            //Debug.Log("[PLAYERINVENTORY] - ItemRequiredForInteraction Count > 0.");
            foreach(ItemData item in pickup.itemRequiredForInteraction)
            {
                //Debug.Log("[PLAYERINVENTORY]  - Request to remove: " + item.ItemType);
                RemoveItem(item.ItemType);
            }
        }

        if (pickup.noninteractableAfterPickUp) //[BRIAN NOTE] changes tag to "untagged" once picked up
        {
            pickup.tag = "Untagged";
        }

        if (pickup.deactivateOnPickUp) //[BRIAN NOTE] deactivates on pickup.
        {
            pickup.gameObject.SetActive(false);
            //Destroy(pickup.gameObject);
        }
    }

    private void AddItem(Items item)
    {
        if (heldItems.Contains(item))
        {
            Debug.LogError("Tried to pick up a duplicate of an item! Operation not supported!");
            return;
        }

        var data = GetItemData(item);
        var icon = Instantiate(iconPrefab, layoutGroup.transform);
        //renames the icon object to the Item Type
        icon.name = data.ItemType.ToString();
        //keeps a reference to each added icon object
        inventoryIcons.Add(icon);

        icon.GetComponent<Image>().sprite = data.Sprite;
        itemIconReferences.Add(item, icon);
        heldItems = itemIconReferences.Keys.ToList();
    }

    private void ReplaceItem(Items itemToRemove, Items itemToAdd)
    {
        var icon = itemIconReferences[itemToRemove];
        itemIconReferences.Remove(itemToRemove);
        itemIconReferences.Add(itemToAdd, icon);
        icon.GetComponent<Image>().sprite = GetItemData(itemToAdd).Sprite;
        heldItems = itemIconReferences.Keys.ToList();
    }

    private void RemoveItem(Items item)
    {
        GameObject itemDestroyed = null;
        heldItems.Remove(item);
        itemIconReferences.Remove(item);
        heldItems = itemIconReferences.Keys.ToList();
        
        //Checks for the iconObject and removes it.
        foreach(GameObject iconObject in inventoryIcons)
        {
            //Debug.Log("[PLAYERINVENTORY] - Checking Item: " + iconObject.name + " against: " + item);

            //compares item to the iconObject name.
            if(iconObject.name == item.ToString())
            {
                //Debug.Log("[PLAYERINVENTORY] - Matching Image Found: " + iconObject.name);
                //takes a reference to the object before destruction;
                itemDestroyed = iconObject;
                Destroy(iconObject);
            }
        }
        if(itemDestroyed != null)
        {
            //removes reference to item objects that have been destroyed.
            inventoryIcons.Remove(itemDestroyed);
        }
    }

    private ItemData GetItemData(Items item)
    {
        return itemData.First(x => x.ItemType == item);
    }
}
