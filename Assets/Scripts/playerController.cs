using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirection))]
public class playerController : MonoBehaviour
{   
    public float walkSpeed = 5f;

    public float runSpeed = 10f;

    public float airSpeed = 8f;

    public float CurrentMoveSpeed{get{
        if(touchingDirection.IsOnWall){
            return 0;
        }
        if(!touchingDirection.IsGround){
            return airSpeed;
        }
        if(IsMoving && !touchingDirection.IsOnWall){
            if(IsRunning){
                return runSpeed;
            }
            return walkSpeed;
        }
        return 0;
    }}
    public float jumpImpulse = 20f;
    Vector2 moveInput = Vector2.zero;

    TouchingDirection touchingDirection;


    [SerializeField]
    private bool _isMoving = false;

    public bool  IsMoving{ get
        {
            return _isMoving;
        } private set
        {
            _isMoving = value;
            animator.SetBool("isMoving",value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;

    [SerializeField]
    private bool _isFacingRight = true;

    public bool IsFacingRight {get {return _isFacingRight;} 
    private set{
        if(_isFacingRight != value){
            transform.localScale *= new Vector2(-1,1);
        }
        _isFacingRight = value;
    }
    
    }

    public bool  IsRunning{ get
        {
            return _isRunning;
        } private set
        {
            _isRunning = value;
            animator.SetBool("isRunning",value);
        }
    }

    Rigidbody2D rb;

    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        touchingDirection = GetComponent<TouchingDirection>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate(){
        rb.velocity = new Vector2(moveInput.x*CurrentMoveSpeed,rb.velocity.y);

        animator.SetFloat("yVelocity",rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context){
        
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }
    public void OnJump(InputAction.CallbackContext context){
        if(context.started && touchingDirection.IsGround){
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x,jumpImpulse);
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        // Check only for horizontal movement, ignore vertical input
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
    public void OnRun(InputAction.CallbackContext context){
        if(context.started){
            IsRunning = true;
        }
        else if(context.canceled){
            IsRunning = false;
        }

    }
}
