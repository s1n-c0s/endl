﻿using System.Collections;
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

    public bool lockMovement;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;

        dashDistance = playerData.dashDistance;
        dashTime = playerData.dashTime;

    }

    void Update()
    {
        GetInput();

        if (canDash && Input.GetKeyDown(KeyCode.Space))
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
            PlayerMovement();

            if (!lockMovement)
            {
                if (moveInput.magnitude != 0)
                    PlayerRotation(); //Rotate kae ton mee kan click mouse
                else
                    FaceMouseClick();
            }
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

        // Check for dash input
        if (canDash && Input.GetKeyDown(KeyCode.Space))
        {
            if (dir.magnitude == 0)
            {
                // If no movement input, dash in the direction the player is facing
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
        Vector3 rotDir = new Vector3(dir.x, dir.y, dir.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rotDir), Time.deltaTime * playerData.rotateSpeed);
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
            }
        }
    }

    private void StartDash()
    {
        if (dir.magnitude == 0) return;
        isDashing = true;
        dashTimer = 0f;
        dashDirection = dir.normalized;
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
}