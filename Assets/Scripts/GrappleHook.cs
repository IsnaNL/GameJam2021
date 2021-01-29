using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{

     float ShortestDistance = 0;
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
     Collider2D[] hookTargets;
     public Collider2D correctTarget;
    private List<float> DistancesFromNodes = new List<float>();
    int index;

    private void Start()
    {
        StartCoroutine(NodesRoutine());
    }
    private void Update()
    {

        if (runningCooldown <= cooldown)
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
            Vector2 dir = new Vector2(correctTarget.transform.position.x - transform.position.x, correctTarget.transform.position.y - transform.position.y).normalized;
            Reached(dir);
            hooked = false;
        }
      
    }

    private void CheckNodes()
    {
        ShortestDistance = 0;
        DistancesFromNodes.Clear();
        hookTargets = Physics2D.OverlapCircleAll(transform.position, GrappleHookDistance, Hookable);
        foreach (Collider2D HT in hookTargets)
        {
            DistancesFromNodes.Add(new Vector2(HT.transform.position.x - transform.position.x, HT.transform.position.y - transform.position.y).magnitude);
            foreach (float d in DistancesFromNodes)
            {
                if (d <= ShortestDistance)
                {
                    ShortestDistance = d;
                }
            }
        }
        index = DistancesFromNodes.IndexOf(ShortestDistance);
        if (hookTargets.Length > 0)
        {
            correctTarget = hookTargets[index + 1];
        }
        else
        {
            correctTarget = null;
        }
    }

    private void FixedUpdate()
    {
        Grapple();
      
    }
    IEnumerator NodesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.06f);
            if (!hooked)
            {
                CheckNodes();
            }
        }
    }
    private void Grapple()
    {
        if (mouseClicked && canRay)
        {
          
          
            if (correctTarget)
            {            
                handrb.velocity = Vector2.zero;
                Vector2 HandCheck = Vector2.MoveTowards(hand.transform.position, correctTarget.transform.position, hookTravelSpeed );
                handrb.MovePosition(HandCheck);
                float curLength = new Vector2(HandCheck.x - handrb.position.x, HandCheck.y - handrb.position.y).magnitude;
                if (curLength < 0.004f)
                {
                    
                    movement.rb.velocity = Vector2.zero;
                    hooked = true;
                    Vector2 Target = Vector2.MoveTowards(transform.position, correctTarget.transform.position, hookTravelSpeed);
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
