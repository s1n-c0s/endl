using UnityEngine;

public class PlayerAttackRobot : MonoBehaviour
{
    public float damageAmount = 10f; // Amount of damage the player's attack does
    public EnemyHealth robotHealth; // Reference to the EnemyHealth script on the robot

    public void OnAttackButtonClick()
    {
        if (robotHealth != null)
        {
            robotHealth.TakeDamage(damageAmount);
            Debug.Log("Attack robot! Damage: " + damageAmount);
            Debug.Log("Robot Health: " + robotHealth.GetCurrentHealth());
        }
    }
}
