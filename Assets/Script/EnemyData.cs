using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName = "Enemy";
    public float maxHealth = 100f;

    [Header("Attack Settings")]
    public float damage = 10f;
    public float attackRange = 10f;
    public float attackSpeed = 1f;

    [Header("Bullet Settings")]
    public float bulletLifeTime = 2f;
    public float bulletSpeed = 10f;    

    public Sprite enemyIcon; // Optional: You can use this to store an icon/image representing the enemy

    // You can add more data here, like movement speed, attack range, etc.
}
