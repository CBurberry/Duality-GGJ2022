using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : Interactable
{
    [BoxGroup("Pickup")]
    [EnumFlags]
    public PlayerInventory.Items item;
}
