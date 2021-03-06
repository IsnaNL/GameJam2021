﻿using System.Collections;
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
                sr.color = new Color(1, 1, 1, 1f);
            }
            else
            {
                particlesystem.gameObject.SetActive(false);
                sr.color = new Color(1, 1, 1, 0.8f);
            }
           
        }
        else
        {
            particlesystem.gameObject.SetActive(false);
            sr.color = new Color(1, 1, 1, 0.8f);
        }
    }
}
