using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEff : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    Vector2 startingPosition;

    // These are now accessed in Update, avoiding recalculation on every use.
    float startingZ;

    // Start is called before the first frame update
    void Start()
    {   
        if (cam == null)
        {
            cam = Camera.main;  // Automatically assigns the main camera
        }
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate cam movement and distance once per frame
        Vector2 camMoveSinceStart = (Vector2)cam.transform.position - startingPosition;
        float distanceFromTarget = transform.position.z - followTarget.position.z;
        float clippingPlane = (cam.transform.position.z + (distanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
        float parallaxFactor = Mathf.Abs(distanceFromTarget) / clippingPlane;

        // Apply the parallax effect
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
