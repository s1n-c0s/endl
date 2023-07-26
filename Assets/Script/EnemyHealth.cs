using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Transform fillImage; // Reference to the custom fill Image's transform

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        HideHealthBar(); // Hide the health bar at the start
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0f);
        UpdateHealthBar();

        if (currentHealth > 0f)
        {
            ShowHealthBar(); // Show the health bar when damaged
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0f;
    }

    void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / maxHealth;
        fillImage.localScale = new Vector3(healthPercentage, 1f, 1f);
    }

    void ShowHealthBar()
    {
        fillImage.gameObject.SetActive(true);
    }

    void HideHealthBar()
    {
        fillImage.gameObject.SetActive(false);
    }
}
