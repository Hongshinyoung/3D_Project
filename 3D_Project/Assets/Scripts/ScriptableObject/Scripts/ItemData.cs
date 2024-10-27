using UnityEngine;

public enum ItmeType
{
    Equipment,
    Consumbable,
    Resource
}


[CreateAssetMenu(fileName = "Item", menuName = "Items")]
public class ItemData : ScriptableObject
{
    [Header("ItemInfo")]
    public string itemName;
    public string itemDescription;
    public ItmeType itmeType;
    public Sprite itemSprite;
    public GameObject dropPrefab;

    [Header("Equip")]
    public GameObject equipPrefab;

    [Header("Consumable")]
    public GameObject consumablePrefab;
}
