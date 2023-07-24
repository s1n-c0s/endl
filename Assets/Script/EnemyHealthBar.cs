using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public EnemyInfo enemyInfo; // Reference to the enemy info data
    public Canvas healthCanvas; // Reference to the canvas used for health display
    public Image healthBarFill; // Reference to the health bar fill image
    public float displayOffset = 2.0f; // Offset from the enemy to display the health bar

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        // Make sure we have all the necessary references
        if (enemyInfo == null || healthCanvas == null || healthBarFill == null)
        {
            Debug.LogError("EnemyHealthDisplay: Some references are not set!");
            enabled = false; // Disable the script to prevent errors
        }

        // Ensure the health bar is initially hidden
        healthCanvas.gameObject.SetActive(false);

        // Set the health bar fill amount to represent the enemy's health
        UpdateHealthBar();
    }

    private void Update()
    {
        // Update the health bar position above the enemy
        if (enemyInfo != null && healthCanvas != null)
        {
            Vector3 worldPos = transform.position + Vector3.up * displayOffset;
            Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);
            healthCanvas.transform.position = screenPos;
        }
    }

    private void UpdateHealthBar()
    {
        // Update the health bar fill amount based on the enemy's current health
        float fillAmount = (float)enemyInfo.currentHealth / enemyInfo.maxHealth;
        healthBarFill.fillAmount = fillAmount;
    }

    public void ShowHealthBar()
    {
        // Show the health bar on top of the enemy
        if (healthCanvas != null)
        {
            healthCanvas.gameObject.SetActive(true);
        }
    }

    public void HideHealthBar()
    {
        // Hide the health bar
        if (healthCanvas != null)
        {
            healthCanvas.gameObject.SetActive(false);
        }
    }
}
