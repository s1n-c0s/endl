using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    //public string playerName = "Player";
    public float maxHealth = 100f;
    public float damage = 10f;
    public Sprite playerIcon; 
}
