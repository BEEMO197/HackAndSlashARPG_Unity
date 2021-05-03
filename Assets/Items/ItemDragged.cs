using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDragged : MonoBehaviour
{
    public ItemSlot itemSlotDraggedFrom;
    public ItemSlot itemSlotHovered;

    public Item itemDragged;

    public void updateItemSlots(Character pc)
    {
        if (itemSlotHovered != null)
        {
            // No Item in hovered Slot, fill item in there
            if (itemSlotHovered.itemInSlot == null)
            {
                bool itemAdded = itemSlotHovered.addItemToSlot(itemSlotDraggedFrom.itemInSlot);

                if(itemAdded)
                    itemSlotDraggedFrom.removeItemInSlot();
                else
                {
                    itemSlotDraggedFrom.addItemToSlot(itemSlotDraggedFrom.itemInSlot);
                }
            }
            // There is an Item in hovered Slot, swap Items, or don't let it happen
            else
            {
                Item tempItem = itemSlotHovered.itemInSlot;
                itemSlotHovered.addItemToSlot(itemSlotDraggedFrom.itemInSlot);
                itemSlotDraggedFrom.addItemToSlot(tempItem);
            }

            itemSlotHovered = null;
        }
        else
        {
            itemSlotDraggedFrom.addItemToSlot(itemSlotDraggedFrom.itemInSlot);
        }
        itemSlotDraggedFrom = null;
        StartCoroutine(resetStats(pc));
    }

    private IEnumerator resetStats(Character pc)
    {
        yield return new WaitForSeconds(Time.deltaTime * 5.0f);
        pc.updateCoreStats();
    }
}
