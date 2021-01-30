using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFly : MonoBehaviour
{
    public float rangeForFly;
    public LayerMask playerLayerMask;
    public AudioClip[] butterflyClips;
    private AudioSource audioSource;
    private Dialog dialogManager;
    public int sentenceIndex;
    bool islookingRight;
    bool reached;
    Vector2 startPos;
    Vector2 vLastPos = Vector3.zero;
    public float rangeForIdleFly;
    public float[] positionsX = new float[2];
    public float[] positionsY = new float[2];
    public float curveSpeed;
    public float moveSpeed;
    float fTime = 0;
    private bool isLookingUp;
    private Animator animator;
    private SpriteRenderer sr;

    private void Start()
    {
        dialogManager = FindObjectOfType<Dialog>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        //audioSource.Play();
        startPos = transform.position;
        positionsX[0] = transform.position.x - rangeForIdleFly;
        positionsX[1] = transform.position.x + rangeForIdleFly;
        positionsY[0] = transform.position.y - rangeForIdleFly * 0.1f;
        positionsY[1] = transform.position.y + rangeForIdleFly * 0.1f;

    }
    void FixedUpdate()
    {   
        CheckForPlayer();
    }
    private void CheckForPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, rangeForFly, playerLayerMask);
        if (player)
        {
            positionsX[0] = transform.position.x - rangeForIdleFly;
            positionsX[1] = transform.position.x + rangeForIdleFly;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 0.05f);
            Vector2 CurLength = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            float CurLengthMag = CurLength.magnitude;
            if (CurLengthMag < 0.004)
            {
                
                StartCoroutine(ReachRoutine());
            }
        }
        else
        {
            if(transform.position.x <= positionsX[0])
            {
                islookingRight = !islookingRight;
               // animator.SetTrigger("Turn");
                sr.flipX = true;
            }
            if(transform.position.x >= positionsX[1])
            {
                islookingRight = !islookingRight;
              //  animator.SetTrigger("Turn");
                sr.flipX = true;
            }
            if (transform.position.y <= positionsY[0])
            {
                isLookingUp = !isLookingUp;
            }
            if (transform.position.y >= positionsY[1])
            {
                isLookingUp = !isLookingUp;
            }
            if (islookingRight)
            {
                transform.position += new Vector3(moveSpeed,0) * Time.fixedDeltaTime;
            }
            else
            {
                transform.position += new Vector3(-moveSpeed, 0) * Time.fixedDeltaTime;
            }
            if (isLookingUp)
            {
                transform.position += new Vector3(0, moveSpeed) * Time.fixedDeltaTime;
            }
            else
            {
                transform.position += new Vector3(0, -moveSpeed) * Time.fixedDeltaTime;
            }
        }
    }
    IEnumerator ReachRoutine()
    {
        if (!reached)
        {
            audioSource.Stop();
            animator.SetTrigger("Reached");
            audioSource.volume = 0.07f;
            audioSource.spatialBlend = 0;
            audioSource.PlayOneShot(butterflyClips[1]);
            reached = true;
            yield return new WaitForSeconds(1.4f);
            dialogManager.StartCoroutine(dialogManager.Type());
            Destroy(this.gameObject);
        }
     
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, rangeForFly);
    }
}
