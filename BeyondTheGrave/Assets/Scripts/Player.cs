using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public float health;
    public float meter;
    public float speed;
    public Vector2 dash;
    public float jumpSpeed;
    //variable that contains all the information regarding what state the character is in
    //includes: standing, crouching, airborne, stunned
    public string charState;
    public bool blocking;
    public string name;
    
    public Player()
    {
        health = 10000f;
        meter = 0f;
        charState = "standing";
        blocking = false;
        name = "Player";
        speed = 4.5f;
        dash = new Vector2(speed*2, 0f);
        jumpSpeed = 4f;
    }

    public Player(float h, string n, float s, Vector2 d, float f)
    {
        health = h;
        meter = 0f;
        charState = "standing";
        blocking = false;
        name = n;
        speed = s;
        dash = d;
        jumpSpeed = f;
    }
}
