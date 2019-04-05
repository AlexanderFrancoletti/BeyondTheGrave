using UnityEngine;
using System.Collections;

public class LightBoxSpawner : MonoBehaviour
{

    public float damage;
    public PlayerController enemy;
    public PlayerController controller;
    // Set these in the editor
    public PolygonCollider2D box1;

    // Used for organization
    private PolygonCollider2D[] colliders;

    // Collider on this game object
    private PolygonCollider2D localCollider;

    hitBoxes val;

    // We say box, but we're still using polygons.
    public enum hitBoxes
    {
        frame2Box,
        clear // special case to remove all boxes
    }

    void Start()
    {
        // Set up an array so our script can more easily set up the hit boxes
        colliders = new PolygonCollider2D[] { box1 };
        val = hitBoxes.frame2Box;
        // Create a polygon collider
        localCollider = gameObject.AddComponent<PolygonCollider2D>();
        localCollider.isTrigger = true; // Set as a trigger so it doesn't collide with our environment
        localCollider.pathCount = 0; // Clear auto-generated polygons
    }

    private void Update()
    {
        //Debug.Log(localCollider.pathCount);
        //Debug.Log(localCollider.points.Length);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (controller.player.MoveUsed[0])
        {
            Debug.Log("Collider hit something!");
            enemy.player.health -= damage;
            Debug.Log(enemy.player.health);
            controller.player.MoveUsed[0] = false;
            controller.HitConfirm = true;
        }
    }

    public void setLightBox()
    {
        //localCollider.pathCount = 0;
        if (val != hitBoxes.clear)
        {
            localCollider.SetPath(0, colliders[(int)val].GetPath(0));
            val++;
            return;
        }
        localCollider.pathCount = 0;
        val = hitBoxes.frame2Box;
    }
}