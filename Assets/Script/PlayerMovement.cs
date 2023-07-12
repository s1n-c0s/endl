using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed = 5f; // Base speed at which the player moves
    public float sprint = 2f; // Multiplier applied to the speed when sprinting

    private NavMeshAgent navAgent;
    private float currentSpeed; // Speed at which the player currently moves

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        currentSpeed = baseSpeed;
        navAgent.speed = currentSpeed;
    }

    private void Update()
    {
        // Check for player input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate movement direction based on player's forward direction
        Vector3 movement = transform.forward * verticalInput + transform.right * horizontalInput;
        movement.Normalize();

        // Set NavMeshAgent destination
        if (movement.magnitude > 0f)
        {
            Vector3 targetPosition = transform.position + movement;
            navAgent.SetDestination(targetPosition);
        }

        // Check for sprint input
        bool sprintInput = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Update the current speed based on sprint input
        currentSpeed = sprintInput ? baseSpeed + sprint : baseSpeed;
        navAgent.speed = currentSpeed;
    }
}
