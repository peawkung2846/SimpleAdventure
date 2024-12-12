using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
public class BossKnight : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate = 0.03f;
    public DetectionZone attackZone;
    public PlayerDetectionZone targetZone;
    //public DetectionZone cliftDetectionZone;

    Rigidbody2D rb;
    TouchingDirection touchingDirection;
    Animator animator;

    public enum WalkableDirection { Left, Right }

    private WalkableDirection _walkDirection = WalkableDirection.Right;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }


            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;
    private bool detectPlayer;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool("hasTarget", value);
        }
    }

    public float AttackCooldown
    {
        get { return animator.GetFloat("attackCooldown"); }
        private set
        {
            animator.SetFloat("attackCooldown", Mathf.Max(value, 0));
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool("canMove"); }
    }

    public bool IsAlive
    {
        get { return animator.GetBool("isAlive"); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectionColliders.Count > 0;
        AttackCooldown -= Time.deltaTime;
        detectPlayer = targetZone.detectionColliders.Count > 0;
        
    }

    private void FixedUpdate()
    {
        Debug.Log(detectPlayer);
        if (detectPlayer)
        {
            if (targetZone.detectionColliders[0].transform.position.x > this.transform.position.x)
            {
                WalkDirection = WalkableDirection.Right;
            }
            else
            {
                WalkDirection = WalkableDirection.Left;
            }
        }

        else if ((touchingDirection.IsOnWall && touchingDirection.IsGround))
        {
            FlipDirection();
            // Debug.Log(cliftDetectionZone.detectionColliders.Count);
        }

        if (CanMove)
        {
            rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }

    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

}
