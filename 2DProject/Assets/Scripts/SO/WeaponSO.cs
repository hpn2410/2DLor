using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName ="Weapon")]
public class WeaponSO : ItemSO
{
    public float damage;
    public float defense;
    public override void WeaponUse()
    {
        PlayerStats playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        if (playerStats.hasWeapon == false)
        {
            playerStats.UseWeapon(this);
            base.WeaponUse();
        }
        else
        {
            playerStats.ChangeWeapon(this);
            base.WeaponUse();
        }
    }
}
