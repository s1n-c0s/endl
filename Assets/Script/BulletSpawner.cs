using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int numBullets = 8;
    public float radius = 5f;
    public Vector3 centerOffset; // Local center point offset

    public EnemyData enemyData; // Reference to the EnemyData

    public bool loop = false; // Whether to respawn bullets after all have been fired

    private WaitForSeconds respawnDelay; // Cached WaitForSeconds for respawn delay

    private void Awake()
    {
        respawnDelay = new WaitForSeconds(enemyData.attackSpeed);
    }

    private void Start()
    {
        SpawnBullets();
    }

    private void SpawnBullets()
    {
        for (int i = 0; i < numBullets; i++)
        {
            float angle = i * (360f / numBullets);
            Vector3 spawnPosition = centerOffset + Quaternion.Euler(0f, angle, 0f) * (Vector3.forward * radius);

            // Calculate the spawn rotation based on the angle
            Quaternion spawnRotation = Quaternion.Euler(0f, angle, 0f);

            // Calculate the forward direction for the bullet (in local space)
            Vector3 forwardDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;

            // Spawn the bullet relative to the enemy's position and rotation
            GameObject bullet = ObjectPoolManager.SpawnObject(bulletPrefab, transform.position + spawnPosition, transform.rotation * spawnRotation, ObjectPoolManager.PoolType.Bullet);

            if (bullet != null)
            {
                // Set bullet properties directly on the BulletController component
                BulletController bulletController = bullet.GetComponent<BulletController>();
                if (bulletController != null)
                {
                    bulletController.bulletSpeed = enemyData.bulletSpeed;
                    bulletController.SetBulletProperties(enemyData.damage, forwardDirection, enemyData.bulletLifeTime);
                }
            }
        }

        if (loop)
        {
            // Call the recursive function to respawn bullets after a delay
            Invoke("RespawnBullets", enemyData.attackSpeed);
        }
    }

    private void RespawnBullets()
    {
        // Return all bullets to the pool
        BulletController[] bullets = FindObjectsOfType<BulletController>();
        foreach (var bullet in bullets)
        {
            ObjectPoolManager.ReturnObjectToPool(bullet.gameObject);
        }

        // Spawn the bullets again
        SpawnBullets();
    }
}
