using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLine : MonoBehaviour
{
    public LineRenderer line;
    Vector3[] positions = new Vector3[2];
    public Transform Hand; 

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        positions[0] = transform.position;
        positions[1] = Hand.position;
        line.SetPositions(positions);
    }
}
