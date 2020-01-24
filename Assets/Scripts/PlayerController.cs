using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 16.0f;
    [SerializeField] private float groundCheckRadius;
    
    private float movementInputDirection;
    private float facingDirection = 1;

    private bool canJump;
    private bool isGrounded;
    private bool isFacingRight = true;
   
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
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
        
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
