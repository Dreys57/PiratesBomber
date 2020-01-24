using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 16.0f;
    [SerializeField] private float groundCheckRadius;
    
    private float movementInputDirection;
    private float facingDirection = 1;

    private bool canJump;
    private bool isWalking;
    private bool isGrounded;
    private bool isFacingRight = true;
    private bool jumpButtonPressed;
   
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        ApplyMovement();
        
        CheckSurroundings();
        
    }

    void Update()
    {
        CheckInput();
        
        CheckIfCanJump();
        
        CheckMovementDirection();
        
        UpdateAnimations();
        
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        
        if(Input.GetButtonDown("Jump"))
        {
            Jump();

            jumpButtonPressed = true;
        }
        else
        {
            jumpButtonPressed = false;
        }
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && body.velocity.y < 0.1f)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if (Math.Abs(body.velocity.x) >= 0.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void ApplyMovement()
    {
        body.velocity = new Vector2(speed * movementInputDirection, body.velocity.y);
    }

    private void Jump()
    {
        if (canJump)
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", body.velocity.y);
        animator.SetBool("jumpButtonPressed", jumpButtonPressed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
