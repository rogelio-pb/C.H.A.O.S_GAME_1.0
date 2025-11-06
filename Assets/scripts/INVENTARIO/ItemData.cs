using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("Datos básicos")]
    public int id;
    public string itemName;
    [TextArea]
    public string description;

    [Header("Visual")]
    public Sprite icon;

    [Header("Stackeo")]
    public bool stackable = true;
    public int maxStack = 99;
}
