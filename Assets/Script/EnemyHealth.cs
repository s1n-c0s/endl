using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public EnemyData enemyData;
    private float currentHealth;

    public Image healthBarImage;

    void Start()
    {
        currentHealth = enemyData.maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, enemyData.maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        float fillAmount = currentHealth / enemyData.maxHealth;
        healthBarImage.fillAmount = fillAmount;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Die()
    {
        // Handle enemy death, e.g., play death animation, destroy enemy, etc.
        Destroy(gameObject);
        Debug.Log(">>>> Destroy Robot");
    }
}
