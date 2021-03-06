﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    float horInput;
    bool groundcheck;
    public float speedModifier;
    public Vector2 jumpForce;
    SpriteRenderer sr;
    Animator animator;
    bool isjump;
    bool isFloating;
    public bool canFloat;
    public bool isLookingRight;
    float savedGravityScale;
    public float floatingGravityScale;
    public float MaxXMagnitude;
    public LayerMask groundLayerMask;
    public ParticleSystem dust;
    public AudioSource AudioSource;
    public AudioClip[] audioClips;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        savedGravityScale =  rb.gravityScale;
    }
    void Update()
    {
        horInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)) {
            isjump = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) && !groundcheck)
        {
            canFloat = true;
          
        }
        if (Input.GetKey(KeyCode.Space) && canFloat) {
            if(rb.velocity.y <= 0)
            {
                 isjump = false;
                 isFloating = true;
                animator.SetBool("Float", true);
            }
        }
        else
        {
            isFloating = false;
            animator.SetBool("Float", false);
        }
        Flip(); 
    }
    private void Flip()
    {
       if(horInput > 0)
       {
            

            isLookingRight = true;
            sr.flipX = !isLookingRight;
       }
       else if (horInput < 0)
       {
            isLookingRight = false;
            sr.flipX = !isLookingRight;
       }
    }
    void FixedUpdate()
    {
        HorMovement();
        Jump();
        CeilingCheck();
        Float();
        ClampVelocity();
    }
    private void ClampVelocity()
    {
        if(rb.velocity.y < 0 && !isFloating)
        {
            animator.SetBool("Falling", true);
            animator.SetBool("Jump", false);
            RaycastHit2D checkForGround = Physics2D.Raycast(transform.position, Vector2.down,1.5f, groundLayerMask);
            Debug.DrawRay(transform.position, Vector2.down *1.5f);
            if (checkForGround )
            {
                animator.SetBool("Falling", false);
            }
        }
        if(rb.velocity.x >= MaxXMagnitude)
        {
            rb.velocity = new Vector2(MaxXMagnitude, rb.velocity.y);
        }else if(rb.velocity.x <= -MaxXMagnitude)
        {
            rb.velocity = new Vector2(-MaxXMagnitude, rb.velocity.y);
        }
    }
    private void CeilingCheck()
    {
        RaycastHit2D ceilingCheck = Physics2D.Raycast(transform.position, Vector2.up);
        if (ceilingCheck.transform.CompareTag("Ground"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
    private void HorMovement()
    {
        if (groundcheck)
        {
            if (horInput != 0)
            {
               /* if (!playWalkSound)
                {
                    AudioSourceWalk.Play();
                    playWalkSound = true;
                }*/
                rb.velocity += new Vector2(horInput * speedModifier, 0);
                if(rb.velocity.x >= 5 || rb.velocity.x <= -5)
                {
                    
                    animator.SetBool("Run", true);
                }
              
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x * 0.9f, rb.velocity.y);
                animator.SetBool("Run", false);
            }
            if (rb.velocity.x > 0 && horInput < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x * 0.85f, rb.velocity.y);
            }
            else if (rb.velocity.x < 0 && horInput > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x * 0.85f, rb.velocity.y);
            }
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Falling", true);
            if (horInput != 0)
            {
                rb.velocity += new Vector2(horInput * speedModifier * 0.3f, 0);
                //animator.SetBool("Run", true);
            }
        }
    }
    private void Jump()
    {
        if (groundcheck && isjump)
        {
            AudioSource.PlayOneShot(audioClips[0]);
            animator.SetBool("Jump", true);
            CreateDust();
            rb.velocity = new Vector2(rb.velocity.x * 0.5f, rb.velocity.y);
            rb.AddForce(jumpForce, ForceMode2D.Impulse);
            isjump = false;
        }
    }
    private void Float()
    {
        if (!groundcheck)
        {
            if (isFloating)
            {
                rb.gravityScale = floatingGravityScale;
              
            }
            else
            {
                rb.gravityScale = savedGravityScale;
              
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            groundcheck = true;
            animator.SetBool("Run", false);
            rb.velocity *= 0.2f;
            rb.gravityScale = savedGravityScale; 
        }

    }

    public void CreateDust()
    {
        dust.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
        {

        if (collision.CompareTag("Ground"))
        {    
            groundcheck = true;
            canFloat = false;
            isFloating = false;
            animator.SetBool("Falling", false);
        }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
         if (collision.CompareTag("Ground"))
         {
            groundcheck = false;
         } 
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, Vector2.up);
        }
}
