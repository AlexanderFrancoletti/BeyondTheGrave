using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public PlayerController playerRef;
    public GameObject hitBoxRefs;
    public LayerMask mask;

    private void Start()
    {
        Debug.Log(playerRef.player.health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.layer == mask)
        //{
        //    playerRef.player.health -= 1000;
        //    Debug.Log(playerRef.player.health);
        //}
    }
}
