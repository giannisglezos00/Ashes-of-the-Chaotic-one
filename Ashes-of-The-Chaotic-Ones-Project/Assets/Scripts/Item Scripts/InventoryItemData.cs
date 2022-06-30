using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

[CreateAssetMenu(menuName ="Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public int ID;
    public string DisplayName;
    [TextArea(4, 4)]
    public string Description;
    public string Rarity;
    public Sprite Icon;
    public int MaxStackSize;
    public bool Consumable; 

}
