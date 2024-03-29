using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    //public string playerName = "Player";
    [Header("Status")]
    public float maxHealth = 100f;
    public float baseDamage = 10f;

    public float maxEnergy = 70f;

    [Header("Speed")]
    public float moveSpeed = 15f;
    public float sprintSpeed = 20f;
    public float rotateSpeed = 5f;

    public float gravity = 25f;
    //public Sprite playerIcon;
    public float moveDistanceOnClick;

    [Header("Dash")]
    public float dashDistance = 10f;
    public float dashTime = 0.3f;
    //public float dashCooldown = 1.0f;
    public float dashCooldownTime = 0.7f;

}