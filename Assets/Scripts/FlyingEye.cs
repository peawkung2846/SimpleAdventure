using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public float flightSpeed = 2f;
    public bool _hasTarget = false;

    int waypointsIndex = 0;

    Transform nextWaypoint;

    public List<Transform> waypoints;

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


    public bool CanMove
    {
        get { return animator.GetBool("canMove"); }
    }



    private void Awake()
    {
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
        float distance =  Vector2.Distance(nextWaypoint.position,transform.position);
        rb.velocity = direction * flightSpeed;
        if(distance <= wayPointReachedDistance){
            waypointsIndex = (waypointsIndex+1)%waypoints.Count;
            nextWaypoint = waypoints[waypointsIndex];
        }

        Vector3 localScale = transform.localScale;

        if(transform.localScale.x > 0){
            if(rb.velocity.x<0){
                transform.localScale = new Vector3(-1*localScale.x,localScale.y,localScale.z);
            }
        }
        else{
            if(rb.velocity.x>0){
                transform.localScale = new Vector3(-1*localScale.x,localScale.y,localScale.z);
            }
        }
    }
}
