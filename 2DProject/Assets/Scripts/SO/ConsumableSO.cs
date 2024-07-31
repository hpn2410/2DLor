using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable Item", menuName = "Consumable/Poision")]
public class ConsumableSO : ItemSO
{
    public bool isPoision = true;
    public override void UseItem()
    {
        base.UseItem();
        PlayerStats playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        playerStats.UseItem();
    }
}
