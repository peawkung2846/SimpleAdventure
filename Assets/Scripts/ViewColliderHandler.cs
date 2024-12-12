using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewColliderHandler : MonoBehaviour
{
    public FlyingEye flyingEye; // Reference to the parent FlyingEye script

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Notify the FlyingEye about the collision
        if (collision.CompareTag("Player")) // Use a tag to identify the player or target
        {
            flyingEye.SetWaypoint(collision.transform.position);
            Debug.Log("Player detected at: " + collision.transform.position);
        }
    }
}