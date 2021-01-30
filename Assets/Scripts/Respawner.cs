using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public Transform curSpawnPoint;
    bool movePlayer;
    Collision2D player;
    public AudioSource audiosource;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            movePlayer = true;
            player = collision;
            audiosource.Play();
        }
    }
    void MovePlayer()
    {
        player.gameObject.GetComponent<Movement>().enabled = false;
        Rigidbody2D curRb = player.gameObject.GetComponent<Rigidbody2D>();
        player.gameObject.GetComponent<Collider2D>().enabled = false;
        player.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        curRb.gravityScale = 0;
        curRb.MovePosition(Vector2.MoveTowards(player.transform.position, curSpawnPoint.position, 1f));
        Vector2 CurLength = new Vector2(curSpawnPoint.position.x - player.transform.position.x, curSpawnPoint.position.y - player.transform.position.y);
        float CurLengthMag = CurLength.magnitude;
        if (CurLengthMag < 0.004)
        {
            curRb.velocity = Vector2.zero;
            player.gameObject.GetComponent<Collider2D>().enabled = true;
            player.gameObject.GetComponent<Movement>().enabled = true;
            player.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;

            curRb.gravityScale = 1;
            movePlayer = false;
        }
    }
    private void FixedUpdate()
    {
        if (movePlayer)
        {
            MovePlayer();
        }
    }
}
