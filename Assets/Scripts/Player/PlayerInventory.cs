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
    private GridLayoutGroup layoutGroup;

    //UI prefab to instantiate in the layout group
    [SerializeField]
    private GameObject iconPrefab;

    //Map of item enum to icon
    private Dictionary<Items, GameObject> itemIconReferences = new Dictionary<Items, GameObject>();

    private string playerInventoryAssetName = "PlayerInventoryUI";

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

        pickup.gameObject.SetActive(false);
        Destroy(pickup.gameObject);
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
        heldItems.Remove(item);
        itemIconReferences.Remove(item);
        heldItems = itemIconReferences.Keys.ToList();
    }

    private ItemData GetItemData(Items item)
    {
        return itemData.First(x => x.ItemType == item);
    }
}
