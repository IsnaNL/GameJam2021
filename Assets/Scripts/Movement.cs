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
    bool isjump;
    bool isFloating;
    bool canFloat;
    float savedGravityScale;
    public float floatingGravityScale;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            }
        }
        else
        {
            isFloating = false;

        }
    }
    void FixedUpdate()
    {
        HorMovement();
        Jump();
        CeilingCheck();
        Float();
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
                rb.velocity += new Vector2(horInput * speedModifier, 0);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x * 0.9f, rb.velocity.y);
            }
            if (rb.velocity.x > 0 && horInput < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x * 0.99f, rb.velocity.y);
            }
            else if (rb.velocity.x < 0 && horInput > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x * 0.99f, rb.velocity.y);
            }
        }
        else
        {
            if (horInput != 0)
            {
                rb.velocity += new Vector2(horInput * speedModifier * 0.6f, 0);
            }
        }
    }
    private void Jump()
    {
        if (groundcheck && isjump)
        {
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
        
        private void OnTriggerStay2D(Collider2D collision)
        {

        if (collision.CompareTag("Ground"))
        {
            rb.gravityScale = savedGravityScale;
            groundcheck = true;
            canFloat = false;
            isFloating = false;
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
