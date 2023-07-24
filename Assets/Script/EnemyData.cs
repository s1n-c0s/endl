using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName = "Enemy";
    public float maxHealth = 100f;
    public float damage = 10f;
    public Sprite enemyIcon; // Optional: You can use this to store an icon/image representing the enemy

    // You can add more data here, like movement speed, attack range, etc.
}
