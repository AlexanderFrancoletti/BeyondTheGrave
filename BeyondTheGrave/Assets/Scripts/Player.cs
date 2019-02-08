using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public float health;
    public float meter;

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
    }

    public Player(float h, string n)
    {
        health = h;
        meter = 0f;
        charState = "standing";
        blocking = false;
        name = n;
    }
}
