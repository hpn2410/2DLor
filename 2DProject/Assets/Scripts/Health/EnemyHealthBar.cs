using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image fillImage; // Reference to the fill image of the health bar
    private EnemyHealth enemyHealth; // Reference to the enemy's stats
    private BossAI bossAI;

    private void Start()
    {
        // Get the EnemyHealth component from the parent gameObject
        enemyHealth = GetComponentInParent<EnemyHealth>();
        bossAI = GetComponentInParent<BossAI>();

        if (enemyHealth != null)
        {
            enemyHealth.OnHealthChanged += UpdateHealthBar; // Subscribe to the health change event
        }
        else if (bossAI != null)
        {
            bossAI.OnHealthChanged += UpdateHealthBar;
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
        if (enemyHealth != null)
        {
            enemyHealth.OnHealthChanged -= UpdateHealthBar; // Unsubscribe to prevent memory leaks
        }
        else if (bossAI != null)
        {
            bossAI.OnHealthChanged -= UpdateHealthBar;
        }
    }
}
