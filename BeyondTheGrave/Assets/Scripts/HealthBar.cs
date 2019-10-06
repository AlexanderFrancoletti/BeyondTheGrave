using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private PlayerController PlayerController;
    private float max_health;
    public string WhichPlayer;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController[] list = FindObjectsOfType<PlayerController>();
        foreach (PlayerController pc in list)
        {
            if (pc.tag == WhichPlayer)
                PlayerController = pc;
        }
        max_health = PlayerController.player.health;
    }

    // Update is called once per frame
    void Update()
    {
        Slider slider = this.GetComponentInParent<Slider>();
        slider.value = PlayerController.player.health / max_health; 
    }
}
