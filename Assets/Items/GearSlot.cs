using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSlot : ItemSlot
{
    public ItemType[] acceptableItemTypes;

    public override bool addItemToSlot(Item newItem)
    {
        for(int i = 0; i < acceptableItemTypes.Length; i++)
        {
            if(newItem.itemType == acceptableItemTypes[i])
            {
                return base.addItemToSlot(newItem);
            }
        }
        return false;
    }
}
