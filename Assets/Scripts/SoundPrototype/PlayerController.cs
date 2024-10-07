using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Movement speed of the player.")]
    [SerializeField] private float moveSpeed = 5f;

    [Tooltip("Force applied when the player jumps.")]
    [SerializeField] private float jumpForce = 5f;

    [Header("Ground Check Settings")]
    [Tooltip("Transform representing the ground check position.")]
    [SerializeField] private Transform groundCheck;

    [Tooltip("Radius of the ground check.")]
    [SerializeField] private float groundCheckRadius = 0.2f;

    [Tooltip("Layers considered as ground.")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject footsteps;

    private Rigidbody2D rb;
    public bool isGrounded;
    private bool isWalking = false; // Tracks if the player is currently walking

    private void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck Transform is not assigned. Please assign it in the Inspector.");
        }
    }

    private void Update()
    {
        // Handle jumping input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // Handle movement input
        Move();

        // Check if the player is grounded
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows

        Vector2 movement = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Determine if the player is walking
        if (Mathf.Abs(moveHorizontal) > 0.01f && isGrounded)
        {
            footsteps.SetActive(true);
            
        }
        else
        {
            footsteps.SetActive(false);
        }
    }

    private void Jump()
    {
        // Apply an upward velocity to the Rigidbody2D
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        SoundManager.Instance.PlayJumpSound();
    }

    // Optional: Visualize the ground check circle in the Editor
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SoundManager.Instance.PlayHitSound();
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        isGrounded = true;
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }
}
