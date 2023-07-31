using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float damage;
    private Vector3 forwardDirection;
    private Rigidbody rb; // Cache the Rigidbody component

    public float bulletSpeed = 10f; // Speed of the bullet

    public void SetBulletProperties(float damageValue, Vector3 direction, float lifeTime)
    {
        damage = damageValue;
        forwardDirection = direction.normalized;

        // Destroy the bullet after a certain amount of time
        Destroy(gameObject, lifeTime);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Cache the Rigidbody component
    }

    void FixedUpdate()
    {
        // Set the velocity of the bullet to move in its local forward direction
        rb.velocity = forwardDirection * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Apply damage to the player or handle player's health here
            other.gameObject.GetComponent<IHealthBar>().TakeDamage(damage);
            Debug.Log("Player Hit! Damage: " + damage);
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            // Destroy the bullet when it collides with anything
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            Debug.Log("Destroy Bullet");
        }
    }
}
