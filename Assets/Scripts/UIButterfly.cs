using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButterfly : MonoBehaviour
{
    Animator animator;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(resetAnim());
    }
    private IEnumerator resetAnim()
    {
        animator.speed = Random.Range(5f, 20f);
        yield return new WaitForEndOfFrame();
        animator.speed = Random.Range(.5f, .8f);
    }
    private void OnDisable()
    {
        animator.speed = 0;
    }
}
