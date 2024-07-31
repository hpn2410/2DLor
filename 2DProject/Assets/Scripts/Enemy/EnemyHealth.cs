using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;
    private float enemyDamage = 25f;
    private KnockBack knockBack;

    [SerializeField] private GameObject[] itemPrefabs;
    [Range(0f, 1f)] private float dropChance = 0.7f;

    public Action<float, float> OnHealthChanged;
    private void Start()
    {
        currentHealth = maxHealth;
        knockBack = GetComponent<KnockBack>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        knockBack.GetKnockBack(PlayerManagement.instance.transform, 15f);
        Debug.Log("Enemy health: " + currentHealth.ToString());
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            TrySpawnItems();
            Destroy(gameObject);
        }
    }

    private void TrySpawnItems()
    {
        // Check if the random value is less than the dropChance
        if (Random.value < dropChance)
        {
            SpawnItems();
        }
    }

    private void SpawnItems()
    {
        // Check if itemPrefabs array is not empty
        if (itemPrefabs.Length > 0)
        {
            // Randomly select an item prefab from the array
            GameObject itemToSpawn = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

            Instantiate(itemToSpawn, transform.position, Quaternion.identity);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            playerStats.TakeDamage(enemyDamage);
            knockBack.GetKnockBack(collision.gameObject.transform, 5f);
        }
    }
}