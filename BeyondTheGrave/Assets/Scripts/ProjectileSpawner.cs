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
        newProj = Instantiate(SProjectile, controller.transform.position, controller.transform.rotation) as GameObject;
        newProj.SetActive(true);
        controller.rb.AddForce(new Vector2((newProj.GetComponent<Projectile>().speed *  -controller.rb.velocity.x) * controller.player.speed*2, 0));
    }

    public void SpawnJ5S()
    {
        newProj = Instantiate(jSProjectile, controller.transform.position, controller.transform.rotation) as GameObject;
        newProj.SetActive(true);
        //controller.rb.AddForce(new Vector2((newProj.GetComponent<Projectile>().speed * -controller.rb.velocity.x) * controller.player.speed * 2, 0));
    }
}
