using UnityEngine;

public enum ItmeType
{
    Equipment,
    Consumbable,
    Resource
}

public class ItemDataConsumable
{
    public float value;
}


[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("ItemInfo")]
    public string itemName;
    public string itemDescription;
    public ItmeType itmeType;
    public Sprite itemSprite;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Equip")]
    public GameObject equipPrefab;

    [Header("Consumable")]
    public ItemDataConsumable consumablePrefab;
}