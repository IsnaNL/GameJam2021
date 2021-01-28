using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{

     
     bool hooked;
     public bool canRay;
     bool  GrappleReadyFromCooldown;
     public Rigidbody2D handrb;
     public Hand hand;  
     public float GrappleHookDistance;
     public float hookTravelSpeed;
     public float hookTravelForce;
     public Movement movement;
     public bool mouseClicked;
     public LayerMask Hookable;
     public float cooldown;
     float runningCooldown;
     public int hookTargetLayer;
     Collider2D HookTarget;

    private void Update()
    { 
        if(runningCooldown <= cooldown)
        {
            canRay = false;
            GrappleReadyFromCooldown = false;
            runningCooldown += Time.deltaTime;
        }else
        {
            GrappleReadyFromCooldown = true;
        }
        if (GrappleReadyFromCooldown)
        {
            canRay = true;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {

            mouseClicked = true;
        }
        else
        {
            mouseClicked = false;
           
           
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && hooked)
        {
            Vector2 dir = new Vector2(HookTarget.transform.position.x - transform.position.x, HookTarget.transform.position.y - transform.position.y).normalized;
            Reached(dir);
            hooked = false;
            Debug.Log("MouseUp");
        }
      
        //Debug.DrawRay(transform.position, Dir * 5);
    }
    private void FixedUpdate()
    {
        Grapple();

    }
    
    private void Grapple()
    {
        if (mouseClicked && canRay)
        {
          
          
            HookTarget = Physics2D.OverlapCircle(transform.position, GrappleHookDistance, Hookable);
            if (HookTarget)
            {            
                handrb.velocity = Vector2.zero;
                Vector2 HandCheck = Vector2.MoveTowards(hand.transform.position, HookTarget.transform.position, hookTravelSpeed );
                handrb.MovePosition(HandCheck);
                float curLength = new Vector2(HandCheck.x - handrb.position.x, HandCheck.y - handrb.position.y).magnitude;
                if (curLength < 0.004f)
                {
                    
                    movement.rb.velocity = Vector2.zero;
                    hooked = true;
                    Vector2 Target = Vector2.MoveTowards(transform.position, HookTarget.transform.position, hookTravelSpeed);
                    movement.rb.MovePosition(Target);
                    Vector2 secCurLength = new Vector2(Target.x - movement.rb.position.x, Target.y - movement.rb.position.y);
                    float secCurLengthMag = secCurLength.magnitude;
                    if(secCurLengthMag < 0.004)
                    {
                        canRay = false;
                        Reached(hand.CalcDir());
                    }
                 
                }
            }
        }
        else
        {
            handrb.position = movement.rb.position;
        }
    }
  
   
    void Reached(Vector2 dir)
    {
        GrappleReadyFromCooldown = false;
        runningCooldown = 0;
        movement.rb.AddForce(dir * hookTravelForce, ForceMode2D.Impulse);
        hooked = false;
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GrappleHookDistance);
    }
}
