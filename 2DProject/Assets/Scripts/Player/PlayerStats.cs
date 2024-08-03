using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public float playerDamage;
    public float playerDefense;
    public bool hasWeapon;
    public ConsumableSO[] consumableSOs;
    //public Animator swordAnim;
    public GameObject[] weaponList;
    private KnockBack knockBack;

    public Action<float, float> OnHealthChanged;

    private void Start()
    {
        knockBack = GetComponent<KnockBack>();
        currentHealth = maxHealth = 100;
        foreach (var item in weaponList)
        {
            item.SetActive(false);
        }
    }

    public void UseItem()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 10;
            Debug.Log("Health 10 HP");
        }

        else
            currentHealth = maxHealth;

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void UseWeapon(WeaponSO weapon)
    {
        maxHealth += weapon.health;
        playerDamage += weapon.damage;
        playerDefense += weapon.defense;
        hasWeapon = true;

        // Set animation for the correct item
        SetWeaponAnimation(weapon.itemName);
    }

    public void ChangeWeapon(WeaponSO newWeapon)
    {
        ResetStats();
        UseWeapon(newWeapon);
    }

    public void ResetStats()
    {
        maxHealth = 100;
        playerDamage = 5;
        playerDefense = 5;
        hasWeapon = false;

        foreach (var item in weaponList)
        {
            item.SetActive(false);
        }
    }

    public void SetWeaponAnimation(string weaponName)
    {
        switch (weaponName)
        {
            case "Hammer":
                weaponList[2].SetActive(true);
                break;
            case "Scythe":
                weaponList[1].SetActive(true);
                break;
            case "Sword":
                weaponList[0].SetActive(true);
                break;
            default:
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth + playerDefense - damage;
        Debug.Log("Player health: " + currentHealth.ToString());
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameManager.instance.EndGame("You have been defeated.");
        }
    }
}
