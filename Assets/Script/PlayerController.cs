using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Transform cam;

    [Header("Move")]
    float speedSmoothVelocity;
    float speedSmoothTime;
    float currentSpeed;
    float velocityY;
    Vector3 moveInput;
    Vector3 dir;


    [Header("Player Data")]
    public PlayerData playerData;

    [Header("Dash")]
    public bool canDash = true;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private Vector3 dashDirection;
    public float dashDistance;
    public float dashTime;
    public float dashCooldownTime;
    private bool isDashingCooldown = false;
    private float dashCooldown;

    public bool lockMovement;
    bool isMouseButtonDown = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;

        dashDistance = playerData.dashDistance;
        dashTime = playerData.dashTime;
        dashCooldownTime = playerData.dashCooldownTime;
    }

    void Update()
    {
        GetInput();

        if (canDash && !isDashingCooldown && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isDashing)
            {
                StartDash();
            }
        }

        if (isDashing)
        {
            UpdateDash();
        }
        else
        {
            if (!lockMovement)
            {
                PlayerMovement();

                if (moveInput.magnitude != 0)
                    PlayerRotation();
            }
        }

        // Check if left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            isMouseButtonDown = true;
            FaceMouseClick();
        }
        // Check if left mouse button is released
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseButtonDown = false;
        }
    }
    private void FixedUpdate()
    {
        // Check if left mouse button is pressed
        if (Input.GetMouseButton(0))
        {
            lockMovement = true;
            isMouseButtonDown = true;
        }
        else
        {
            lockMovement = false;
            isMouseButtonDown = false;
        }
    }

    private void GetInput()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        dir = (forward * moveInput.y + right * moveInput.x).normalized;

        if (canDash && !isDashingCooldown && Input.GetKeyDown(KeyCode.Space))
        {
            if (dir.magnitude == 0)
            {
                dir = transform.forward;
            }

            StartDash();
        }
    }

    private void PlayerMovement()
    {
        currentSpeed = Mathf.SmoothDamp(currentSpeed, playerData.moveSpeed, ref speedSmoothVelocity, speedSmoothTime * Time.deltaTime);

        if (velocityY > -10) velocityY -= Time.deltaTime * playerData.gravity;
        Vector3 velocity = (dir * currentSpeed) + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
    }

    private void PlayerRotation()
    {
        if (dir.magnitude == 0) return;

        Vector3 rotDir = new Vector3(dir.x, 0, dir.z); // Ignore the vertical component
        Quaternion targetRotation = Quaternion.LookRotation(rotDir); // Calculate target rotation

        // Adjust the rotation speed based on your preference
        float rotationSpeed = playerData.rotateSpeed * Time.deltaTime * 10;

        // Apply the rotation more quickly using Slerp
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
    }

    private void FaceMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 lookDir = hit.point - transform.position;
                lookDir.y = 0;
                transform.rotation = Quaternion.LookRotation(lookDir);

                // Move the player a little forward after clicking
                Vector3 targetPosition = transform.position + transform.forward * playerData.moveDistanceOnClick;
                controller.Move(targetPosition - transform.position);
            }
        }
    }

    private void StartDash()
    {
        if (dir.magnitude == 0) return;
        isDashing = true;
        dashTimer = 0f;
        dashDirection = dir.normalized;

        // Start the dash cooldown
        StartCoroutine(DashCooldown());
    }

    private void UpdateDash()
    {
        dashTimer += Time.deltaTime;
        if (dashTimer >= dashTime)
        {
            isDashing = false;
        }
        else
        {
            controller.Move(dashDirection * dashDistance / dashTime * Time.deltaTime);
        }
    }

    private IEnumerator DashCooldown()
    {
        isDashingCooldown = true;
        yield return new WaitForSeconds(dashCooldownTime);
        isDashingCooldown = false;
    }
}
