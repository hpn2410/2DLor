using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    private bool isBagOpen;
    public ItemSlot[] itemSlot;
    //public PlayerStats playerStats;
    //public ItemSO[] itemSOs;
    //public WeaponSO[] weaponSOs;
    private Vector3 dropPos = new Vector3(1.5f, 0, 0);
    private Vector3 dropSize = new Vector3(.5f, .5f, .5f);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && !isBagOpen)
        {
            Time.timeScale = 0;
            Debug.Log("Open");
            isBagOpen = true;
            inventoryMenu.SetActive(true);
        }
        else if (Input.GetButtonDown("Inventory") && isBagOpen)
        {
            Time.timeScale = 1;
            Debug.Log("Close");
            isBagOpen = false;
            inventoryMenu.SetActive(false);
        }
    }

    public void UseItem()
    {
        foreach (var oneSlot in itemSlot)
        {
            if (!oneSlot.isEmpty && oneSlot.isChoose)
            {
                // after update data, check the value of the itemSlot
                if (oneSlot.itemType.isWeapon && oneSlot.isChoose)
                    oneSlot.itemType.WeaponUse();
                else if (oneSlot.itemType.isConsumable && oneSlot.isChoose)
                    oneSlot.itemType.UseItem();
                DecreaseQuantity(oneSlot);
                break;
            }
        }
    }

    public void DropButton(string itemName, int quantity, Sprite itemSprite, string itemDes, ItemSO itemType)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isEmpty && itemSlot[i].isChoose)
            {
                // Create a new item
                GameObject itemToDrop = new GameObject(itemSlot[i].itemName);
                itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + dropPos;
                itemToDrop.transform.localScale = dropSize;

                // Add components to the new item GameObject
                Item newItem = itemToDrop.AddComponent<Item>();
                newItem.quantity = 1;
                newItem.itemName = itemSlot[i].itemName;
                newItem.itemSprite = itemSlot[i].itemSprite;
                newItem.itemDes = itemSlot[i].itemDes;
                newItem.itemType = itemSlot[i].itemType;

                // Create a new Sprite Renderer
                SpriteRenderer newItemSR = itemToDrop.AddComponent<SpriteRenderer>();
                newItemSR.sprite = itemSlot[i].itemSprite;
                newItemSR.sortingLayerName = "Ground";
                newItemSR.sortingOrder = 5;

                // Add box Collider 2d
                itemToDrop.AddComponent<BoxCollider2D>();

                Debug.Log("Item has been dropped");

                DecreaseQuantity(itemSlot[i]);
                break;
            }
        }
    }

    public void DecreaseQuantity(ItemSlot slot)
    {
        // Decrease the item quantity
        slot.quantity--;
        // Update quantity text and check if the slot is empty
        if (slot.quantity <= 0)
        {
            slot.EmptySlot();
        }
        else
        {
            slot.quantityText.text = slot.quantity.ToString();
        }
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDes, ItemSO itemType)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName
                || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDes, itemType);
                if (leftOverItems > 0)
                    return leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDes, itemType);
                return leftOverItems;
            }
        }
        return quantity;
    }

    public void DeSelectAllItemSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].isChoose = false;
            itemSlot[i].useButton.SetActive(false);
            itemSlot[i].dropButton.SetActive(false);
        }
    }
}
