using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    Vector2 startPoint;

    [SerializeField]
    Vector2 endPoint;

    public Inventory inventory;
    public Character playerCharacter;

    public Item itemInSlot;
    public int ItemCount;
    public Image itemImage;
    public ItemDragged itemDrag;

    public FollowMouse fm;

    public bool occupied = false;

    public void Start()
    {
        if (itemInSlot == null)
        {
            itemImage.gameObject.SetActive(false);
            occupied = false;
        }
        else
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = itemInSlot.ItemIcon;
            occupied = true;
        }

        inventory = FindObjectOfType<Inventory>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log("Mouse Entered: " + name);

        if (itemDrag.itemSlotDraggedFrom != null)
            itemDrag.itemSlotHovered = this;

        if (itemInSlot != null)
        {
            switch (itemInSlot.itemType)
            {
                case ItemType.WEAPON:
                case ItemType.ARMOR:
                case ItemType.ACCESSORY:
                    fm.updateDescriptions(itemInSlot as Gear);
                    break;

                default:
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Debug.Log("Mouse Exit: " + name);

        itemDrag.itemSlotHovered = null;

        if (itemInSlot != null)
        {
            switch (itemInSlot.itemType)
            {
                case ItemType.WEAPON:
                case ItemType.ARMOR:
                case ItemType.ACCESSORY:
                    fm.updateDescriptions(null);
                    break;

                default:
                    break;
            }
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("Mouse Down: " + name);
        if (itemInSlot != null)
        {
            itemDrag.itemSlotDraggedFrom = this;
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log("Mouse Up: " + name);
        itemDrag.updateItemSlots(playerCharacter);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPoint = eventData.pressPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemInSlot != null)
        {
            endPoint = eventData.position;
            itemImage.rectTransform.position = endPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public bool hasItemInSlot()
    {
        return itemInSlot != null;
    }

    public virtual bool addItemToSlot(Item newItem)
    {
        occupied = true;
        itemImage.gameObject.SetActive(true);
        itemInSlot = newItem;
        itemImage.sprite = itemInSlot.ItemIcon;
        centerItemImage();

        return true;
        //inventory.updateItemSlots();
    }

    public void removeItemInSlot()
    {
        occupied = false;
        itemImage.gameObject.SetActive(false);
        itemInSlot = null;
        itemImage.sprite = null;
    }

    // Centers items around other slots, depending on the items X and Y Sizes
    public void centerItemImage()
    {
        float xScale = itemInSlot.SizeX;
        float yScale = itemInSlot.SizeY;

        itemImage.rectTransform.position = new Vector3(transform.position.x + ((54.25f * --xScale) / 2), transform.position.y - ((54.25f * --yScale) / 2));
        itemImage.rectTransform.localScale = new Vector3(itemInSlot.SizeX, itemInSlot.SizeY, 1.0f);
    }
}
