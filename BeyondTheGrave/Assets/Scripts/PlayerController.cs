using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player player;
    private int jump;
    private Vector2 directionalInput;
    private float jumpInput;
    private Vector2 Velocity;
    private float friction;
    private float halfHeight;
    public bool grounded;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        player = new Player();
        jump = 1;
        friction = player.speed*2;
        halfHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        grounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfHeight - 0.04f), Vector2.down, 0.025f);
    }

    // Update is called once per frame
    void Update()
    {
        jumpInput = Input.GetAxis("Jump");
        directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (grounded)
        {
            rb.AddForce(new Vector2((directionalInput.x * friction - rb.velocity.x) * friction, 0));
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            if (jumpInput > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, player.jumpSpeed * 6f);
                grounded = false;
            }
        }
        else
        {
            rb.AddForce(new Vector2((directionalInput.x/2 * friction - rb.velocity.x) * friction, 0));
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        grounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfHeight - 0.04f), Vector2.down, 0.025f);
    }
}
