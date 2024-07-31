using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="ItemToUse")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public float health;
    public float mana;
    public bool isConsumable = true;
    public bool isWeapon = true;

    public virtual void WeaponUse()
    {
        Debug.Log("Using weapon: " + itemName); // Log itemName
    }

    public virtual void UseItem()
    {
        Debug.Log("Using item: " + itemName); // Log itemName
    }
}
