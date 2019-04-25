using System.Collections;
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
            enemy.GetComponent<PlayerController>().player.health -= damage;
            Debug.Log(enemy.GetComponent<PlayerController>().player.health);
            controller.GetComponent<PlayerController>().player.MoveUsed[moveId] = false;
            controller.GetComponent<PlayerController>().HitConfirm = true;
            enemy.GetComponent<PlayerController>().player.charState = "hitstun";
            enemy.GetComponent<PlayerController>().stunTime = stunMod * 2;
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.layer == mask)
            Destroy(this.gameObject);
    }
}
