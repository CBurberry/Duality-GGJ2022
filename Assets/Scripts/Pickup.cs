using NaughtyAttributes;

public class Pickup : Interactable
{
    [BoxGroup("Pickup")]
    [EnumFlags]
    public PlayerInventory.Items item;
}
