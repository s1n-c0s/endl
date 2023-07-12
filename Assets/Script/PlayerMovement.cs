using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent navAgent;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on player's forward direction
        Vector3 movement = transform.forward * verticalInput + transform.right * horizontalInput;
        movement.Normalize();

        // Set NavMeshAgent destination
        if (movement.magnitude > 0f)
        {
            Vector3 targetPosition = transform.position + movement;
            navAgent.SetDestination(targetPosition);
        }
    }
}
