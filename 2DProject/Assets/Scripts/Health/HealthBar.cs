using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage; // Reference to the fill image of the health bar
    private PlayerStats playerStats; // Reference to the player's stats

    private void Start()
    {
        // Assuming PlayerStats script is on the player
        playerStats = FindObjectOfType<PlayerStats>();

        if (playerStats != null)
        {
            playerStats.OnHealthChanged += UpdateHealthBar; // Subscribe to the health change event
        }
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = currentHealth / maxHealth;
        }
    }

    private void OnDestroy()
    {
        if (playerStats != null)
        {
            playerStats.OnHealthChanged -= UpdateHealthBar; // Unsubscribe to prevent memory leaks
        }
    }
}
