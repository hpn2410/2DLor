using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropButtonScript : MonoBehaviour
{
    public ItemSlot itemSlot;
    private Vector3 dropPos = new Vector3(1.5f, 0, 0);
    private Vector3 dropSize = new Vector3(.5f, .5f, .5f);
    public void DropItem(string itemName, int quantity, Sprite itemSprite, string itemDes, ItemSO itemType)
    {
        // Update info
        itemSlot.itemName = itemName;
        itemSlot.itemSprite = itemSprite;
        itemSlot.itemImage.sprite = itemSprite;
        itemSlot.itemImage.enabled = true;
        itemSlot.itemDes = itemDes;
        itemSlot.itemType = itemType;

        if (string.IsNullOrEmpty(itemName) || itemSprite == null)
        {
            Debug.LogWarning("Cannot Drop the item");
            return;
        }
        // Create a new item
        GameObject itemToDrop = new GameObject(itemName);
        // Set location to drop
        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position
            + dropPos;
        // Set size of itemDrop
        itemToDrop.transform.localScale = dropSize;

        // Add components to the new item GameObject
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.itemSprite = itemSprite;
        newItem.itemDes = itemDes;
        newItem.itemType = itemType;

        // Create a new Sprite Renderer
        SpriteRenderer newItemSR = itemToDrop.AddComponent<SpriteRenderer>();
        newItemSR.sprite = itemSprite;
        newItemSR.sortingLayerName = "Ground";
        newItemSR.sortingOrder = 5;

        // Add box Collider 2d
        itemToDrop.AddComponent<BoxCollider2D>();


        Debug.Log("item has been dropped");

        // Subject the item
        itemSlot.quantity--;
        itemSlot.quantityText.text = itemSlot.quantity.ToString();
        if (itemSlot.quantity <= 0)
        {
            EmptySlot();
        }
    }

    public void EmptySlot()
    {
        itemSlot.isFull = false;
        itemSlot.quantityText.enabled = false;
        itemSlot.itemImage.sprite = itemSlot.emptySprite;

        itemSlot.desNameText.text = "";
        itemSlot.desText.text = "";
        itemSlot.desImage.sprite = itemSlot.emptySprite;

        itemSlot.itemName = "";
        itemSlot.itemSprite = null;
        itemSlot.itemDes = "";
    }
}
