using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab for the bullet GameObject
    public int numBullets = 8; // Number of bullets to spawn
    public float radius = 5f; // Radius of the circle where bullets are spawned
    public Vector3 centerOffset; // Local center point offset
    public EnemyData enemyData; // Reference to the EnemyData scriptable object
    public bool loop = false; // Whether to respawn bullets after all have been fired

    private WaitForSeconds respawnDelay; // Cached WaitForSeconds for respawn delay
    private Transform bulletParent; // Parent object to hold bullets

    private void Awake()
    {
        respawnDelay = new WaitForSeconds(enemyData.attackSpeed);
        bulletParent = new GameObject("Bullets").transform; // Create a new empty GameObject to hold all the spawned bullets
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
            Quaternion spawnRotation = Quaternion.Euler(0f, angle, 0f);

            // Spawn the bullet using the ObjectPoolManager
            GameObject bullet = ObjectPoolManager.SpawnObject(bulletPrefab, transform.position + spawnPosition, spawnRotation, ObjectPoolManager.PoolType.Bullet);

            if (bullet != null)
            {
                // Get the BulletController component and set its properties
                BulletController bulletController = bullet.GetComponent<BulletController>();
                if (bulletController != null)
                {
                    bulletController.bulletSpeed = enemyData.bulletSpeed;
                    // Set bullet properties directly on the BulletController component
                    bulletController.SetBulletProperties(enemyData.damage, spawnRotation * Vector3.forward, enemyData.bulletLifeTime);
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
        // Return all bullets to the pool by iterating through the bulletParent and calling ObjectPoolManager.ReturnObjectToPool
        foreach (Transform bullet in bulletParent)
        {
            ObjectPoolManager.ReturnObjectToPool(bullet.gameObject);
        }

        // Spawn the bullets again
        SpawnBullets();
    }
}
