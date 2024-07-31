using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public GameObject selectedShader;
    public bool isChoose;
    private InventoryManager inventoryManager;

    [Header("------------------Item Data--------------------")]
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public bool isEmpty = true;
    public string itemDes;
    public Sprite emptySprite;
    public ItemSO itemType;

    [Header("------------------Item Slot--------------------")]
    public int maxNumberItems = 99;
    public TMP_Text quantityText;
    public Image itemImage;
    public GameObject useButton;
    public GameObject dropButton;

    [Header("------------------Item Slot--------------------")]
    public Image desImage;
    public TMP_Text desNameText;
    public TMP_Text desText;

    private void Awake()
    {
        inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDes, ItemSO itemType)
    {
        // check to see if the slot is full
        if (isFull)
            return quantity;

        // Update info
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;
        this.itemDes = itemDes;
        this.itemType = itemType;

        //Update Quantity
        this.quantity += quantity;
        isEmpty = false;

        // if quantity item over maxNumberItems
        if (this.quantity >= maxNumberItems)
        {
            quantityText.text = maxNumberItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            // Return leftOver
            int extraItems = this.quantity - maxNumberItems;
            this.quantity = maxNumberItems;
            return extraItems;
        }
        // if not
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;
        return 0;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.DeSelectAllItemSlots();
        selectedShader.SetActive(true);
        isChoose = true;
        desNameText.text = itemName;
        desText.text = itemDes;
        desImage.sprite = itemSprite;
        if (desImage.sprite == null)
            desImage.sprite = emptySprite;

        // Hide btn
        useButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void OnRightClick()
    {
        if (isEmpty)
        {
            Debug.LogWarning("Cannot use or drop item, slot is empty");
            return;
        }

        inventoryManager.DeSelectAllItemSlots();
        selectedShader.SetActive(true);
        isChoose = true;
        desNameText.text = itemName;
        desText.text = itemDes;
        desImage.sprite = itemSprite;
        if (desImage.sprite == null)
            desImage.sprite = emptySprite;

        if (!string.IsNullOrEmpty(itemName))
        {
            useButton.SetActive(true);
            dropButton.SetActive(true);
        }
    }

    public void UseItem()
    {
        inventoryManager.UseItem();
    }

    public void DropItem()
    {
        inventoryManager.DropButton(itemName, quantity, itemSprite, itemDes, itemType);
    }

    public void EmptySlot()
    {
        isFull = false;
        isEmpty = true;
        quantityText.enabled = false;
        itemImage.sprite = emptySprite;

        desNameText.text = "";
        desText.text = "";
        desImage.sprite = emptySprite;

        itemName = "";
        itemSprite = null;
        itemDes = "";
    }
}
