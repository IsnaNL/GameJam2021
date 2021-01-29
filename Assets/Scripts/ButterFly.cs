using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFly : MonoBehaviour
{
    public float rangeForFly;
    public LayerMask playerLayerMask;
    public ParticleSystem Idle;
    public ParticleSystem Reached;
    private Dialog dialogManager;
    public int sentenceIndex;
    private void Start()
    {
        dialogManager = FindObjectOfType<Dialog>();
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
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 0.015f);
            Vector2 CurLength = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            float CurLengthMag = CurLength.magnitude;
            if (CurLengthMag < 0.004)
            {
                Idle.Stop();
                Reached.transform.position = player.transform.position;
                StartCoroutine(ReachRoutine());
            }
        }
    }
    IEnumerator ReachRoutine()
    {
        Reached.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.1f);
        dialogManager.StartCoroutine(dialogManager.Type(sentenceIndex));
        Reached.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, rangeForFly);
    }
}
