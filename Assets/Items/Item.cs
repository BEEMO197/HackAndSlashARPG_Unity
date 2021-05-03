using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    COMMON,
    UNCOMMON,
    RARE,
    EPIC,
    LEGENDARY,
    COUNT
}

public enum ItemType
{
    DEFAULT,
    POTION,
    WEAPON,
    ARMOR,
    ACCESSORY
}

[CreateAssetMenu(fileName = "Item", menuName = "Item/Item", order = 1)]
public class Item : ScriptableObject
{
    public Sprite ItemIcon;
    public ItemType itemType;
    public string Description;
    public int ItemCount;
    public int MaxItemCount;

    public characterClass[] UseableClasses;
    public Rarity Rarity;

    [Range(1, 2)]
    public int SizeX;
    [Range(1, 2)]
    public int SizeY;
}
