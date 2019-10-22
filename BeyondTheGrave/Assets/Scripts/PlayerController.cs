using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float AnimFinish;
    private PlayerController p2;

    public HashSet<string> ValidStates;
    public bool HitConfirm;
    private int LastMove;
    private bool[] UsedAlready;
    public float stunTime;
    public float blockStun;
    public int combo;
    public Text comboCount;

    public int speedMod = 5;

    // Start is called before the first frame update
    void Awake()
    {
        player = new Player();

        player.MoveUsed = new bool[18];
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
        friction = player.speed*2*speedMod;
        halfHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        grounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfHeight - 0.04f), Vector2.down, 0.025f);
        anim = GetComponent<Animator>();
        Light = false;
        Heavy = false;
        Special = false;
        Feint = false;
        Idle = true;
        AnimFinish = 0f;
        HitConfirm = false;
        combo = 0;
        stunTime = 0f;
    }

    public void Start()
    {
        foreach (PlayerController p in FindObjectsOfType<PlayerController>())
        {
            if (p.tag != this.tag)
                p2 = p;
        }
    }

    // Update is called once per frame
    void Update()
    {
        jumpInput = Input.GetAxisRaw(VerticalControl);
        directionalInput = new Vector2(Input.GetAxisRaw(HorizontalControl), Input.GetAxisRaw(VerticalControl));
        if ((directionalInput.x < 0 && (transform.position.x < p2.transform.position.x)) || (directionalInput.x > 0 && (transform.position.x > p2.transform.position.x)))
        {
            if (stunTime < 0 && p2.combo < 1)
                player.blocking = true;
            else
                player.blocking = false;
        }
        else
        {
            player.blocking = false;
        }
        if (stunTime <= 0 && blockStun <= 0)
        {
            if (AnimFinish > 0)
            {
                if (HitConfirm)
                {
                    if (Input.GetButtonDown(LightButton))
                    {
                        Light = true;
                        AnimFinish = .25f;
                        if (grounded)
                        {
                            player.MoveUsed[0] = true;
                            LastMove = 0;
                        }
                        else
                        {
                            player.MoveUsed[2] = true;
                            LastMove = 2;
                        }
                    }
                    else if (Input.GetButtonDown(HeavyButton))
                    {
                        Heavy = true;
                        AnimFinish = .417f;
                        if (grounded && directionalInput.x == 0 && directionalInput.y == 0)
                        {
                            player.MoveUsed[1] = true;
                            LastMove = 1;
                        }
                        else if (grounded && directionalInput.x == 0 && directionalInput.y == -1) { 
                            player.MoveUsed[4] = true;
                            LastMove = 4;
                            AnimFinish = .5f;
                        }
                        else
                        {
                            player.MoveUsed[3] = true;
                            LastMove = 3;
                        }
                    }
                }
                if (LastMove == 4)
                {
                    if (jumpInput > 0f)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, player.jumpSpeed * 6f);
                        grounded = false;
                    }
                }
            }
            if (grounded && AnimFinish <= 0f)
            {
                if (directionalInput.y == 0)
                {
                    rb.AddForce(new Vector2((directionalInput.x * friction - rb.velocity.x) * friction, 0));
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                }
                if (jumpInput > 0f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, player.jumpSpeed * 6f);
                    grounded = false;
                }
                if (grounded)
                {
                    if (Input.GetButtonDown(LightButton) && !Input.GetButton(FakeButton))
                    {
                        Light = true;
                        AnimFinish = .25f;
                        player.MoveUsed[0] = true;
                        LastMove = 0;
                    }
                    else if (Input.GetButtonDown(HeavyButton) && !Input.GetButton(FakeButton))
                    {
                        Heavy = true;
                        if (Input.GetAxis(VerticalControl) < -.5f)
                        {
                            AnimFinish = .5f;
                            player.MoveUsed[4] = true;
                            LastMove = 4;
                        }
                        else
                        {
                            AnimFinish = .417f;
                            player.MoveUsed[1] = true;
                            LastMove = 1;
                        }
                    }
                    else if (Input.GetButtonDown(SpecialButton) && !Input.GetButton(FakeButton))
                    {
                        Special = true;
                        AnimFinish = .417f;
                        player.MoveUsed[5] = true;
                        LastMove = 5;
                    }
                    else if (Input.GetButton(FakeButton))
                    {
                        if (Input.GetButtonDown(LightButton))
                            Light = true;
                        else if (Input.GetButtonDown(HeavyButton))
                            Heavy = true;
                        else if (Input.GetButtonDown(SpecialButton))
                            Special = true;
                        Feint = true;
                    }
                }
            }
            else if (!grounded && AnimFinish <= 0f)
            {
                if (Input.GetButtonDown(LightButton) && !Input.GetButton(FakeButton))
                {
                    Light = true;
                    AnimFinish = .25f;
                    player.MoveUsed[2] = true;
                    LastMove = 2;
                }
                else if (Input.GetButtonDown(HeavyButton) && !Input.GetButton(FakeButton))
                {
                    Heavy = true;
                    AnimFinish = .417f;
                    player.MoveUsed[3] = true;
                    LastMove = 3;
                }
                else if (Input.GetButtonDown(SpecialButton) && !Input.GetButton(FakeButton))
                {
                    Special = true;
                    AnimFinish = .417f;
                    player.MoveUsed[6] = true;
                    LastMove = 6;
                }
                else if (Input.GetButton(FakeButton))
                {
                    if (Input.GetButtonDown(LightButton))
                        Light = true;
                    else if (Input.GetButtonDown(HeavyButton))
                        Heavy = true;
                    else if (Input.GetButtonDown(SpecialButton))
                        Special = true;
                    Feint = true;
                }
            }   //Should only get called when the player has movement control
            else if (AnimFinish <= 0f)
            {
                rb.AddForce(new Vector2((directionalInput.x / 2 * friction - rb.velocity.x) * friction, 0));
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }
            if (AnimFinish <= 0f)
            {
                for (int i = 0; i < player.MoveUsed.Length; ++i)
                {
                    player.MoveUsed[i] = false;
                }
                HitConfirm = false;
                LastMove = -1;
            }
        }
        if (combo >= 2)
        {
            comboCount.text = combo + " hits";
        }
        else
            comboCount.text = "";
        grounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfHeight - 0.04f), Vector2.down, 0.025f);
        stunTime -= Time.deltaTime;
        blockStun -= Time.deltaTime;
        if (stunTime > 0)
            Debug.Log("In stun");
        else
            Debug.Log("No stun");
        if (Input.GetButtonUp(FakeButton))
            Feint = false;
        anim.SetBool("Grounded", grounded);
        anim.SetBool("HitConfirm", HitConfirm);
        anim.SetFloat("VerInput", directionalInput.y);
        anim.SetFloat("HorInput", directionalInput.x);
        anim.SetBool("Feint", Feint);
        ExecuteAnim();
        Debug.Log(LastMove);
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
            anim.SetTrigger("Special");
            anim.SetBool("Idle", false);
            Special = false;
        }
        else if (Feint)
            anim.SetBool("Idle", false);
        else if (AnimFinish >= 0)
        {
            AnimFinish -= Time.deltaTime;
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("Idle", true);
        }
        //Debug.Log(AnimFinish);
    }
}
