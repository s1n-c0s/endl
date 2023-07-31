using UnityEngine;
using UnityEngine.UI;

public class IHealthBar : MonoBehaviour
{
    private float currentHealth;
    public Image healthBarImage;

    // Reference to DataManager
    private DataManager dataManager;

    // Add a bool to specify whether this health bar is for the player or enemy
    public bool isPlayerHealthBar = false;

    void Start()
    {
        dataManager = DataManager.Instance;

        if (isPlayerHealthBar)
        {
            currentHealth = dataManager.PlayerData.maxHealth;
        }
        else
        {
            currentHealth = dataManager.EnemyData.maxHealth;
        }

        UpdateHealthBar();
    }

    public float TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        // If the health bar is for the player, clamp the health accordingly to player's max health.
        if (isPlayerHealthBar)
        {
            currentHealth = Mathf.Clamp(currentHealth, 0f, dataManager.PlayerData.maxHealth);
        }
        // Otherwise, clamp the health to enemy's max health.
        else
        {
            currentHealth = Mathf.Clamp(currentHealth, 0f, dataManager.EnemyData.maxHealth);
        }

        UpdateHealthBar();

        if (IsDead())
        {
            Die();
        }

        return damageAmount; // Return the actual damage taken
    }

    private void UpdateHealthBar()
    {
        float maxHealth = isPlayerHealthBar ? dataManager.PlayerData.maxHealth : dataManager.EnemyData.maxHealth;
        float fillAmount = currentHealth / maxHealth;
        healthBarImage.fillAmount = fillAmount;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public bool IsDead()
    {
        return currentHealth <= 0f;
    }

    private void Die()
    {
        // Handle character death, e.g., play death animation, destroy character, etc.
        Destroy(gameObject);
        Debug.Log(">>>> Destroy Character");
    }
}
