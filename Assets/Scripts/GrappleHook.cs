using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    
     Vector2 mouseposWorld;
     bool hooked;
     Vector2 Dir;
     public float GrappleHookDistance;
     public float hookTravelSpeed;
     public Movement movement;
     public bool mouseClicked;
     public LayerMask Hookable;
    // Start is called before the first frame update
    private void Update()
    { 
        mouseposWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Dir = new Vector2(mouseposWorld.x -transform.position.x, mouseposWorld.y - transform.position.y).normalized;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            mouseClicked = true;
        }
        else
        {
            mouseClicked = false;
        }
        Debug.DrawRay(transform.position, Dir * 2);
    }
    private void FixedUpdate()
    {
        if (mouseClicked)
        {
            RaycastHit2D HookRay = Physics2D.Raycast(transform.position, Dir, GrappleHookDistance,Hookable);
            if (HookRay)
            {
              
                Vector2 newTarget = Vector2.MoveTowards(transform.position, HookRay.transform.position, hookTravelSpeed);
                movement.rb.MovePosition(newTarget);
            }
        }
      
        
    }
}
