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
    private float timer;
    private float modeTimerMax = 5f;
    private float zoneTimerMax = 5f;
    private float rushTimerMax = 5f;
    private float distToOpponent;
    private float distToCorner;

    // Start is called before the first frame update
    void Start()
    {
        timer = modeTimerMax;
        int styleCode = ((int)(Random.value * 3));
        Debug.Log(styleCode);
        if (styleCode == 0)
            mode = "footsies";
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
        //controlledCharacter.useGroundSpecial();
        //This mode should try to stay at long ranges and shoot projectiles
        if (mode == "zoning")
        {
            distToOpponent = controlledCharacter.transform.position.x - controlledCharacter.p2.transform.position.x;
            if (distToOpponent < 15f && distToOpponent > 0)
            {
                //Move away from the player
                controlledCharacter.directionalInput = new Vector2(1, 0);
            }
            else if (distToOpponent > -15f && distToOpponent < 0)
            {
                //Also move away from the player
                controlledCharacter.directionalInput = new Vector2(-1, 0);
                if (distToOpponent > -5f)
                {
                    controlledCharacter.directionalInput = new Vector2(-1, -1);
                }
            }
            else
            {
                //Use projectiles
                controlledCharacter.useGroundSpecial();
            }
        }

        //This mode should try to get in close and use fast attacks
        if (mode == "rushdown")
        {
            distToOpponent = controlledCharacter.transform.position.x - controlledCharacter.p2.transform.position.x;
            //Move to opponent
            if (distToOpponent > 1f)
            {
                controlledCharacter.directionalInput = new Vector2(-1, 0);
            }
            else if (distToOpponent < -1f)
            {
                controlledCharacter.directionalInput = new Vector2(1, 0);
            }
            //Do a jump in
            if (distToOpponent > 4f && distToOpponent < 6f)
            {
                int mix = (int)(Random.value * 2);
                if (mix == 1)
                {
                    controlledCharacter.directionalInput = new Vector2(-1, 1);
                    controlledCharacter.useJumpHeavy();
                }
            }
            else if (distToOpponent < -4f && distToOpponent > 6f)
            {
                int mix = (int)(Random.value * 2);
                if (mix == 1)
                {
                    controlledCharacter.directionalInput = new Vector2(1, 1);
                }
            }

            if (distToOpponent > -1f && distToOpponent < 1f)
            {
                if (controlledCharacter.grounded)
                    controlledCharacter.useGroundLight();
                else
                {
                    int mix = (int)(Random.value * 3);
                    if (mix == 0)
                        controlledCharacter.useJumpLight();
                    else if (mix == 1)
                        controlledCharacter.useJumpHeavy();
                    else
                        controlledCharacter.useJumpSpecial();
                }
            }
            
        }

        //This mode tries to play a spacing game
        if (mode == "footsies")
        {
            distToOpponent = controlledCharacter.transform.position.x - controlledCharacter.p2.transform.position.x;
            if (distToOpponent > 2f)
            {
                controlledCharacter.directionalInput = new Vector2(-1, 0);
            }
            else if (distToOpponent < -2f)
            {
                controlledCharacter.directionalInput = new Vector2(1, 0);
            }
            int mix = (int)(Random.value * 2)*-1;
            if (distToOpponent < 2f && distToOpponent > -2f)
            {
                if (distToOpponent > 0)
                    controlledCharacter.directionalInput = new Vector2(1, 0);
                else
                    controlledCharacter.directionalInput = new Vector2(-1, 0);
            }
            if ((distToOpponent < 3f && distToOpponent > 1f) || (distToOpponent < -1f && distToOpponent > -3f))
            {
                controlledCharacter.useGroundHeavy(new Vector2(controlledCharacter.directionalInput.x, mix));
            }
        }

        timer -= Time.deltaTime;
        //After a certain amount of time, swap goals
        if (timer <= 0)
        {
            int styleCode = ((int)(Random.value * 3));
            Debug.Log(styleCode);
            if (styleCode == 0)
                mode = "footsies";
            else if (styleCode == 1)
                mode = "zoning";
            else if (styleCode == 2)
                mode = "rushdown";
            timer = modeTimerMax;
        }
    }
}
