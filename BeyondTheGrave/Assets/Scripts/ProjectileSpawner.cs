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
        if (controller.transform.localScale.x < 0)
        {
            Debug.Log("Go left");
            newProj = Instantiate(SProjectile, controller.transform.position + new Vector3(-2, 0, 0), controller.transform.rotation) as GameObject;
            newProj.GetComponent<Projectile>().direction.x = newProj.GetComponent<Projectile>().direction.x * -1;
        }
        else
        {
            Debug.Log("Go right");
            newProj = Instantiate(SProjectile, controller.transform.position + new Vector3(2, 0, 0), controller.transform.rotation) as GameObject;
        }
        newProj.GetComponent<Projectile>().controller = controller.gameObject;
        newProj.GetComponent<Projectile>().enemy = this.GetComponentInParent<PlayerController>().p2.gameObject;
        newProj.SetActive(true);
        controller.rb.AddForce(new Vector2((controller.player.speed - controller.rb.velocity.x) * controller.player.speed * 50, 0));
    }

    public void SpawnJ5S()
    {
        if (controller.transform.localScale.x < 0)
        {
            newProj = Instantiate(jSProjectile, controller.transform.position + new Vector3(-2, -2, 0), controller.transform.rotation) as GameObject;
            newProj.GetComponent<Projectile>().direction.x = newProj.GetComponent<Projectile>().direction.x * -1;
        }
        else
        {
            newProj = Instantiate(jSProjectile, controller.transform.position + new Vector3(2, -2, 0), controller.transform.rotation) as GameObject;
        }
        newProj.GetComponent<Projectile>().controller = controller.gameObject;
        newProj.GetComponent<Projectile>().enemy = this.GetComponentInParent<PlayerController>().p2.gameObject;
        newProj.SetActive(true);
        controller.rb.AddForce(new Vector2((controller.player.speed - controller.rb.velocity.x) * controller.player.speed * 25, (controller.player.speed - controller.rb.velocity.x) * controller.player.speed * -25));
    }
}
