using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController Player1;
    public PlayerController Player2;
    public float timer;
    public int DisplayTime;
    public Text display;
    // Start is called before the first frame update
    void Start()
    {
        timer = 100f;
        DisplayTime = (int)timer;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTime = (int)timer;
        timer -= Time.deltaTime;
        display.text = DisplayTime + "";
        if (Player1.player.health <= 0 && Player2.player.health <= 0)
        {
            SceneManager.LoadScene("Win Screen");
            Debug.Log("Double Knockout");
        }
        else if (Player2.player.health <= 0)
        {
            SceneManager.LoadScene("Win Screen");
            Debug.Log("Player 1 Win");
        }
        else if (Player1.player.health <= 0)
        {
            SceneManager.LoadScene("Win Screen");
            Debug.Log("Player 2 Win");
        }
        if (timer <= 0f)
        {
            if (Player1.player.health == Player2.player.health)
            {
                SceneManager.LoadScene("Win Screen");
                Debug.Log("Draw!");
            }
            else if (Player2.player.health < Player1.player.health)
            {
                SceneManager.LoadScene("Win Screen");
                Debug.Log("Player 1 Win by time!");
            }
            else if (Player1.player.health < Player2.player.health)
            {
                SceneManager.LoadScene("Win Screen");
                Debug.Log("Player 2 Win by time!");
            }
        }
    }
}
