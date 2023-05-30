using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;       // The force applied to the player when jumping
    public float slideSpeed = 0.5f;        // The speed at which the player slides
    public float moveSpeed = 0.5f;      // Move speed
    public float slideDuration = 0.5f;    // The duration of the slide animation
    public LayerMask groundLayer;        // The layer mask for the ground objects

    private bool isGrounded = false;     // Whether the player is currently on the ground
    private bool isJumping = false;      // Whether the player is currently jumping
    private bool isSliding = false;      // Whether the player is currently sliding
    private bool isRunning = false;      // Whether the player is currently running
    private Rigidbody2D rb;              // The player's Rigidbody2D component
    private Animator anim;               // The player's Animator component

    public Collider2D slideCollider;
    public float horizontalInput;

    private bool isStartedRunning = false;

    public AudioSource jumpAudio;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
    }

    void FixedUpdate()
    {
        // Check if the player is on the ground
        isGrounded = Physics2D.Raycast(transform.position, -Vector2.up, 0.5f, groundLayer);


        // Check if the player is jumping or sliding and adjust animation accordingly
        if (isJumping)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                anim.Play("Jump");
            }

            if (isGrounded)
            {
                isJumping = false;
                

                if (!isRunning && !isSliding && !anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    anim.Play("Idle");
                }
            }
        }
        else if (isSliding)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
            {
                anim.Play("Slide");
            }
        }
        else
        {
            if (!isRunning && !anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                anim.Play("Idle");
            }
        }

        // Check if the player is running and adjust animation accordingly
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            isRunning = true;

            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Run") && !isSliding)
            {
                anim.Play("Run");
            }

            if(horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            isRunning = false;

            if (!isJumping && !isSliding && !anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                anim.Play("Idle");
            }
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        if (isSliding)
        {
            if (horizontalInput != 0)
            {
                rb.velocity = new Vector2(horizontalInput * slideSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            }
        }
    }



    void Update()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isSliding)
        {
            // Apply the jump force to the player
            if(isGrounded && !isJumping && this.gameObject.transform.position.y <= 3.5f)
            {
                jumpAudio.Play();
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = true;
            }
        }

        // Check for slide input
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded && !isJumping)
        {
            // Start the slide animation
            StartCoroutine(Slide());
        }    

    }


    IEnumerator Slide()
    {
        // Set the sliding flag to true and play the slide animation
        isSliding = true;
        anim.Play("Slide");
        rb.velocity = Vector2.zero;

        // Disable the player's Collider2D component to avoid collisions during the slide
        Collider2D playerCollider = GetComponent<Collider2D>();
        playerCollider.enabled = false;

        
        slideCollider.enabled = true;

        // Wait for the slide animation to finish
        yield return new WaitForSeconds(slideDuration);

        // Enable the player's Collider2D component to resume collisions
        playerCollider.enabled = true;
        slideCollider.enabled = false;

        // Set the slideing flag to false
        isSliding = false;
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

}

