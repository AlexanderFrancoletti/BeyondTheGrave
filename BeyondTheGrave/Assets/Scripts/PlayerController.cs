using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player player;
    private int jump;
    private Vector2 directionalInput;
    private float jumpInput;
    private float lastJumpInput;
    private Vector2 Velocity;
    private float friction;
    private float halfHeight;
    public bool grounded;
    public Rigidbody2D rb;
    public string HorizontalControl;
    public string VerticalControl;
    public string LightButton;
    public string HeavyButton;
    public string SpecialButton;
    public string FakeButton;

    public Animator anim;
    private bool Light;
    private bool Heavy;
    private bool Special;
    private bool Feint;
    private bool Idle;
    private float AnimFinish;

    public HashSet<string> ValidStates;
    private int LastMove;
    private bool[] UsedAlready;

    // Start is called before the first frame update
    void Awake()
    {
        player = new Player();

        player.MoveUsed = new bool[4];
        ValidStates = new HashSet<string>();
        ValidStates.Add("standing");
        ValidStates.Add("crouching");
        ValidStates.Add("hitstun");
        ValidStates.Add("hitlag");
        ValidStates.Add("airborn");
        for (int i = 0; i < player.MoveUsed.Length; ++i)
        {
            player.MoveUsed[i] = false;
        }
        UsedAlready = (bool[]) player.MoveUsed.Clone();
        Debug.Log(UsedAlready.Length);
        for (int i = 0; i < UsedAlready.Length; ++i)
        {
            Debug.Log(UsedAlready[i]);
        }

        jump = 1;
        friction = player.speed*2;
        halfHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        grounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfHeight - 0.04f), Vector2.down, 0.025f);
        anim = GetComponent<Animator>();
        Light = false;
        Heavy = false;
        Special = false;
        Feint = false;
        Idle = true;
        AnimFinish = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        jumpInput = Input.GetAxisRaw(VerticalControl);
        directionalInput = new Vector2(Input.GetAxisRaw(HorizontalControl), Input.GetAxisRaw(VerticalControl));
        if (grounded && AnimFinish <= 0f)
        {
            rb.AddForce(new Vector2((directionalInput.x * friction - rb.velocity.x) * friction, 0));
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            if (jumpInput > 0f && lastJumpInput <= 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, player.jumpSpeed * 6f);
                grounded = false;
            }
            if (grounded)
            {
                if (Input.GetButtonDown(LightButton))
                {
                    Light = true;
                    AnimFinish = .25f;
                    player.MoveUsed[0] = true;
                }
                else if (Input.GetButtonDown(HeavyButton))
                {
                    Heavy = true;
                    AnimFinish = .417f;
                    player.MoveUsed[1] = true;
                }
            }
        }
        else if (!grounded && AnimFinish <= 0f)
        {
            if (Input.GetButtonDown(LightButton))
            {
                Light = true;
                AnimFinish = .25f;
                player.MoveUsed[2] = true;
            }
            else if (Input.GetButtonDown(HeavyButton))
            {
                Heavy = true;
                AnimFinish = .417f;
                player.MoveUsed[3] = true;
            }
        }
        else if (AnimFinish <= 0f)
        {
            rb.AddForce(new Vector2((directionalInput.x / 2 * friction - rb.velocity.x) * friction, 0));
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        grounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfHeight - 0.04f), Vector2.down, 0.025f);

        lastJumpInput = Input.GetAxisRaw(VerticalControl);
        anim.SetBool("Grounded", grounded);
        ExecuteAnim();
    }

    public void ExecuteAnim()
    {
        if (Light)
        {
            anim.SetTrigger("Light");
            anim.SetBool("Idle", false);
            Light = false;
        }
        else if (Heavy)
        {
            anim.SetTrigger("Heavy");
            anim.SetBool("Idle", false);
            Heavy = false;
        }
        else if (Special)
        {
            anim.SetBool("Idle", false);
        }
        else if (Feint)
        {
            anim.SetBool("Idle", false);
        }
        else if (AnimFinish >= 0)
        {
            AnimFinish-= Time.deltaTime;
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("Idle", true);
        }
        //Debug.Log(AnimFinish);
    }
}
