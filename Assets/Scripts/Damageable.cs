using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    Animator animator;

    public UnityEvent<int, Vector2> damageableHit;

    [SerializeField]
    private float _maxHealth = 100;

    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private float _health = 100;

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if(_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;



    private float timeSinceHit = 0;
    public float invincibilityTimer = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool("isAlive", value);
            Debug.Log(value);
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool("lockVelocity");
        }
        set
        {
            animator.SetBool("lockVelocity", value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if(timeSinceHit > invincibilityTimer)
            {
                isInvincible=false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
        //Hit(10);
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (_isAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger("hit");
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvent.characterDamaged.Invoke(gameObject,damage);
            return true;
        }
        return false;
    }
}
