using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemSlot[] itemSlots;

    public void Start()
    {
        System.Array.Reverse(itemSlots);
    }

    public void updateItemSlots()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].hasItemInSlot())
            {
                Debug.Log("Item Found: " + itemSlots[i].hasItemInSlot());
                // Check both times
                if (itemSlots[i].itemInSlot.SizeX > 1 && itemSlots[i].itemInSlot.SizeY > 1)
                {

                    for (int j = 0; j < itemSlots[i].itemInSlot.SizeX - 1; j++)
                    {
                        itemSlots[i + j + 1].itemInSlot = itemSlots[i].itemInSlot;
                        itemSlots[i + j + 1].itemImage = itemSlots[i].itemImage;
                        itemSlots[i + j + 1].occupied = true;

                        for (int k = 0; k < itemSlots[i].itemInSlot.SizeY - 1; k++)
                        {
                            itemSlots[i + j + (k + 1) * 7].itemInSlot = itemSlots[i].itemInSlot;
                            itemSlots[i + j + (k + 1) * 7].itemImage = itemSlots[i].itemImage;
                            itemSlots[i + j + (k + 1) * 7].occupied = true;
                        }
                    }
                }
                else
                {
                    // Check Only Y
                    if (itemSlots[i].itemInSlot.SizeY > 1)
                    {
                        for (int j = 1; j < itemSlots[i].itemInSlot.SizeY; j++)
                        {
                            itemSlots[i + 7].itemInSlot = itemSlots[i].itemInSlot;
                            itemSlots[i + 7].itemImage = itemSlots[i].itemImage;
                            itemSlots[i + 7].occupied = true;

                        }
                    }
                    // Check Only X
                    if (itemSlots[i].itemInSlot.SizeX > 1)
                    {
                        for (int j = 1; j < itemSlots[i].itemInSlot.SizeX; j++)
                        {
                            itemSlots[i + j].itemInSlot = itemSlots[i].itemInSlot;
                            itemSlots[i + j].itemImage = itemSlots[i].itemImage;
                            itemSlots[i + j].occupied = true;
                        }
                    }
                }
                break;
            }
        }
    }
}
