using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    Rigidbody2D rb;

    public ContactFilter2D castFilter;
    CapsuleCollider2D touchingCol;

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x>0?(Vector2.right):(Vector2.left);

    private Animator animator;

    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    public bool IsGround {get{

        return _isGround;
    }private set{
        _isGround = value;
        animator.SetBool("isGround",value);
    }}
    public bool IsOnCeiling {get{

        return _isOnCeiling;
    }private set{
        _isOnCeiling = value;
        animator.SetBool("isOnCeiling",value);
    }}
    public bool IsOnWall {get{
        return _isOnWall;
    }private set{
        _isOnWall = value;
        animator.SetBool("isOnWall",value);
    }}

    [SerializeField]
    private bool _isGround ;

    [SerializeField]
    private bool _isOnWall ;

    [SerializeField]
    private bool _isOnCeiling ;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsGround = touchingCol.Cast(Vector2.down,castFilter,groundHits, groundDistance)>0; // touchingCol.Cast return number of object that collide
        IsOnWall = touchingCol.Cast(wallCheckDirection,castFilter,wallHits,wallDistance)>0;
        IsOnCeiling = touchingCol.Cast(Vector2.up,castFilter,ceilingHits,ceilingDistance)>0;
    }

}
