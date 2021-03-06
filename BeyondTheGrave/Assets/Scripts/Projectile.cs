﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 direction = new Vector2(0, 0);
    public float damage = 500;
    public int moveId;
    public float stunMod = .6f;
    public GameObject enemy;
    public GameObject controller;
    public LayerMask mask;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Rigidbody2D>().MovePosition((Vector2)this.gameObject.transform.position+direction*speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == enemy)
        {
            if (enemy.GetComponentInParent<PlayerController>().player.blocking)
            {
                enemy.GetComponentInParent<PlayerController>().blockStun = stunMod / 3 * 2;
                enemy.GetComponentInParent<PlayerController>().player.charState = "blockstun";
            }
            else
            {
                enemy.GetComponentInParent<PlayerController>().player.health -= damage;
                Debug.Log(enemy.GetComponentInParent<PlayerController>().player.health);
                controller.GetComponent<PlayerController>().player.MoveUsed[moveId] = false;
                controller.GetComponent<PlayerController>().HitConfirm = true;
                enemy.GetComponentInParent<PlayerController>().player.charState = "hitstun";
                enemy.GetComponentInParent<PlayerController>().stunTime = stunMod * 2;
            }
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.layer == mask)
            Destroy(this.gameObject);
    }
}
