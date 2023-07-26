using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public float attackDamage = 20f; // The amount of damage the player deals per click

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
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                DecreaseEnemyHealth(enemyHealth);
            }
        }
    }

    void DecreaseEnemyHealth(EnemyHealth enemyHealth)
    {
        enemyHealth.TakeDamage(attackDamage);

        if (enemyHealth.IsDead())
        {
            Destroy(enemyHealth.gameObject);
        }
    }
}
