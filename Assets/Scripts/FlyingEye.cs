using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public float flightSpeed = 2f;
    public bool _hasTarget = false;

    int waypointsIndex = 0;

    Transform nextWaypoint;
    public Transform viewTransform;

    public List<Transform> waypoints;

    private Collider2D viewCollider;

    public bool HasTarget { get { return _hasTarget; } private set
        {
            _hasTarget = value;
            animator.SetBool("hasTarget", value);
        }
    }
    public DetectionZone biteDetectionZone;

    public float wayPointReachedDistance = 0.1f;
    Animator animator;
    Rigidbody2D rb;

    Damageable damageable;

    private float waypointTimer = 0f; // Timer to track time spent trying to reach the current waypoint
    private const float waypointTimeout = 5f; // Timeout in seconds



    public bool CanMove
    {
        get { return animator.GetBool("canMove"); }
    }



    private void Awake()
    {
        viewCollider = viewTransform.GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        // touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void Start(){
        nextWaypoint = waypoints[waypointsIndex];
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = biteDetectionZone.detectionColliders.Count>0 ;
    }

    private void FixedUpdate(){
        if(damageable.IsAlive){
            if(CanMove){
                Flight();
            }
            else{
                rb.velocity = Vector3.zero;
            }
        }
        else{
            rb.gravityScale = 2f;
        }
    }

    private void Flight(){
        Vector2 direction = (nextWaypoint.position - transform.position).normalized;
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        rb.velocity = direction * flightSpeed;

        waypointTimer += Time.deltaTime;

        if (distance <= wayPointReachedDistance)
        {
            waypointTimer = 0f;
            waypointsIndex = (waypointsIndex + 1) % waypoints.Count;
            nextWaypoint = waypoints[waypointsIndex];
        }
        else if (waypointTimer >= waypointTimeout)
        {
            // Debug.LogWarning("Waypoint timeout reached. Resetting to default waypoint.");
            waypointTimer = 0f;
            nextWaypoint = waypoints[waypointsIndex];
        }

        Vector3 localScale = transform.localScale;

        if (transform.localScale.x > 0 && rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
        }
        else if (transform.localScale.x < 0 && rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
        }
    }

       public void SetWaypoint(Vector3 position)
    {
        Transform waypoint = new GameObject("PlayerDetectedWaypoint").transform;
        waypoint.position = position;
        nextWaypoint = waypoint;
    }
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
