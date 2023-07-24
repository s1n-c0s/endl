using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyInfo enemyInfo; // Reference to the enemy info data

    private int currentHealth;

    private void Start()
    {
        // Initialize current health with max health from enemy info
        currentHealth = enemyInfo.maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Handle damage calculation using enemy's defense stat
        int actualDamage = Mathf.Max(damage - enemyInfo.defense, 0);
        currentHealth -= actualDamage;

        // Check if the enemy is defeated
        if (currentHealth <= 0)
        {
            Defeat();
        }
    }

    private void Defeat()
    {
        // Logic to handle enemy defeat
        Destroy(gameObject);
    }
}
