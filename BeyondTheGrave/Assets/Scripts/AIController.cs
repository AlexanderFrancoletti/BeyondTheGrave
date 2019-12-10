using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public bool active = false;
    public PlayerController controlledCharacter;
    public Input input;
    //The mode string is used to determine what strategy the AI controlled player is going to employ
    private string mode;
    private float distToOpponent;
    private float distToCorner;

    // Start is called before the first frame update
    void Start()
    {
        int styleCode = ((int)(Random.value * 3));
        Debug.Log(styleCode);
        if (styleCode == 0)
            mode = "balanced";
        else if (styleCode == 1)
            mode = "zoning";
        else if (styleCode == 2)
            mode = "rushdown";
        this.enabled = active;
    }

    // Update is called once per frame
    void Update()
    {
        //Given each strategy, give the AI a goal location on the screen, a preferred method of attack, and give all 3 the ability to defend simple attacks

        //This mode should try to stay at long ranges and shoot projectiles
        if (mode == "zoning")
        {

        }

        //This mode should try to get in close and use fast attacks
        if (mode == "rushdown")
        {

        }

        //This mode should employ a combination of moves, swapping from long to short range along with offense and defense
        if (mode == "balanced")
        {

        }
    }
}
