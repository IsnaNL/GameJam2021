using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
     GrappleHook gH;
    SpriteRenderer sr;
    public ParticleSystem particlesystem;
    // Start is called before the first frame update
    void Start()
    {
        gH = FindObjectOfType<GrappleHook>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(gH.correctTarget != null)
        {
            if (gH.correctTarget.transform == this.transform )
            {
                particlesystem.gameObject.SetActive(true);
            }
            else
            {
                particlesystem.gameObject.SetActive(false);
            }
           
        }
        else
        {
            particlesystem.gameObject.SetActive(false);
        }
    }
}
