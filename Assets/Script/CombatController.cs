/*using UnityEngine;

public class CombatController : MonoBehaviour
{
    public float attackDamage = 20f; // The amount of damage the player deals per click
    public float attackRange = 3f; // The maximum distance at which the player can attack an enemy

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            HandleMouseClick();
        }
    }

    void HandleMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the clicked object is an enemy
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // Check if the enemy is within attack range
                if (IsEnemyWithinAttackRange(enemyHealth.transform.position))
                {
                    DealDamageToEnemy(enemyHealth);
                }
                else
                {
                    Debug.Log("Enemy is too far to attack.");
                }
            }
        }
    }

    bool IsEnemyWithinAttackRange(Vector3 enemyPosition)
    {
        Vector3 playerPosition = transform.position;
        float distance = Vector3.Distance(playerPosition, enemyPosition);
        return distance <= attackRange;
    }

    void DealDamageToEnemy(EnemyHealth enemyHealth)
    {
        enemyHealth.TakeDamage(attackDamage);

        if (enemyHealth.IsDead())
        {
            Destroy(enemyHealth.gameObject);
        }

        Debug.Log("Player dealt " + attackDamage + " damage to the enemy. Enemy's remaining health: " + enemyHealth.GetCurrentHealth());
    }
}*/

/*using UnityEngine;

public class CombatController : MonoBehaviour
{
    public float attackDamage = 20f; // The amount of damage the player deals per click
    public float attackRange = 3f; // The maximum distance at which the player can attack an enemy

    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            HandleMouseClick();
        }
    }

    void HandleMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the clicked object is an enemy
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // Check if the enemy is within attack range
                if (IsEnemyWithinAttackRange(enemyHealth.transform.position))
                {
                    // Rotate the player character to face the attack position
                    playerController.RotateToAttackPosition(hit.point);

                    // Deal damage to the enemy
                    DealDamageToEnemy(enemyHealth);
                }
                else
                {
                    Debug.Log("Enemy is too far to attack.");
                }
            }
        }
    }

    bool IsEnemyWithinAttackRange(Vector3 enemyPosition)
    {
        Vector3 playerPosition = transform.position;
        float distance = Vector3.Distance(playerPosition, enemyPosition);
        return distance <= attackRange;
    }

    void DealDamageToEnemy(EnemyHealth enemyHealth)
    {
        enemyHealth.TakeDamage(attackDamage);

        if (enemyHealth.IsDead())
        {
            Destroy(enemyHealth.gameObject);
        }

        Debug.Log("Player dealt " + attackDamage + " damage to the enemy. Enemy's remaining health: " + enemyHealth.GetCurrentHealth());
    }
}*/