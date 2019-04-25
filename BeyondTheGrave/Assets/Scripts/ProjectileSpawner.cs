using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject SProjectile;
    public GameObject jSProjectile;
    public PlayerController controller;
    private GameObject newProj;

    public void Spawn5S()
    {
        newProj = Instantiate(SProjectile, controller.transform.position+new Vector3(2, 0, 0), controller.transform.rotation) as GameObject;
        newProj.SetActive(true);
        controller.rb.AddForce(new Vector2((controller.player.speed - controller.rb.velocity.x) * controller.player.speed * 50, 0));
    }

    public void SpawnJ5S()
    {
        newProj = Instantiate(jSProjectile, controller.transform.position+new Vector3(2, -2, 0), controller.transform.rotation) as GameObject;
        newProj.SetActive(true);
        controller.rb.AddForce(new Vector2((controller.player.speed - controller.rb.velocity.x) * controller.player.speed * 25, (controller.player.speed - controller.rb.velocity.x) * controller.player.speed * -25));
    }
}
