using UnityEngine;

public class TestDMG : MonoBehaviour
{
    public IHealthBar healthBar;
    public PlayerData playerData;
    public EnemyData enemyData;

    public void OnAttackButtonClick()
    {
        if (healthBar != null)
        {
            if (playerData != null && healthBar.isPlayerHealthBar)
            {
                healthBar.TakeDamage(enemyData.damage);
                Debug.Log("Attack Player! Damage: " + enemyData.damage);
                Debug.Log("Player Health: " + healthBar.GetCurrentHealth());
            }
            else if (enemyData != null && !healthBar.isPlayerHealthBar)
            {
                healthBar.TakeDamage(playerData.baseDamage);
                Debug.Log("Attack Enemy! Damage: " + playerData.baseDamage);
                Debug.Log("Enemy Health: " + healthBar.GetCurrentHealth());
            }
        }
    }
}
