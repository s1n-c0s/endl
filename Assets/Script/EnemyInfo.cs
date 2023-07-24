using UnityEngine;

[System.Serializable]
public class EnemyInfo : MonoBehaviour
{
    // Enemy name and description
    public string enemyName = "Enemy";
    public string description = "This is an enemy.";

    // Enemy stats
    public int maxHealth = 100;
    public int attackPower = 10;
    public int defense = 5;

    public int currentHealth;

    // Add any additional enemy properties as needed
}
