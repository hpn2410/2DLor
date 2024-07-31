using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour
{
    public string itemName;

    public int quantity;

    public Sprite itemSprite;

    [TextArea]
    public string itemDes;

    public ItemSO itemType;

    private InventoryManager inventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            AudioScript.instance.PlaySFX("Collect");
            int lefOverItems = inventoryManager.AddItem(itemName, quantity, itemSprite, itemDes, itemType);
            if(lefOverItems <=  0)
                Destroy(gameObject);
            else
                quantity = lefOverItems;
        }
    }
}
