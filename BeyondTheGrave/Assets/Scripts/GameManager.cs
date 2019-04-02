using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController Player1;
    public PlayerController Player2;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player1.player.health <= 0 && Player2.player.health <= 0)
        {
            Debug.Log("Double Knockout");
        }
        else if (Player2.player.health <= 0)
        {
            Debug.Log("Player 1 Win");
        }
        else if (Player1.player.health <= 0)
        {
            Debug.Log("Player 2 Win");
        }
    }
}
