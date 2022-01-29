using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple data class to represent an item in the player's inventory.
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public PlayerInventory.Items ItemType;
    public Sprite Sprite;
}
