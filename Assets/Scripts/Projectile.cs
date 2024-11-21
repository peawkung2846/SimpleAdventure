using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector2 moveSpeed = new Vector2(3f,0f);

    public Vector2 knockback = Vector2.zero;

    Rigidbody2D rb;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collider2D){
        Damageable damageable = collider2D.GetComponent<Damageable>();
        if(damageable != null){
            Vector2 kb = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.Hit(damage, kb);
            if (gotHit) 
                Debug.Log(collider2D.name + " hit for " +  damage);
                Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
