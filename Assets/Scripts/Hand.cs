using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool handHit;
    public GrappleHook grapple;
    Vector2 dir;
   
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {  
        handHit = true;
        dir = new Vector2(transform.position.x - grapple.transform.position.x, transform.position.y - grapple.transform.position.y).normalized;
       // gameObject.GetComponent<Rigidbody2D>().position = gameObject.GetComponentInParent<Rigidbody2D>().position;

    }

    public Vector2 CalcDir()
    {
        return dir;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        handHit = false;
    }
}
