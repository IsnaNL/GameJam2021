using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] float magnitude;
    [SerializeField] float speed;
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * magnitude * Mathf.Sin(Time.time * speed) * Time.deltaTime;
    }
}
