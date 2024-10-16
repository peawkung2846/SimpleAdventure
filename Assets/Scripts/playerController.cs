using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class playerController : MonoBehaviour
{   
    public float walkSpeed = 5f;
    Vector2 moveInput;

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
        rb.velocity = new Vector2(moveInput.x*walkSpeed,rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context){
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
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
            walkSpeed = 8;
        }
        else if(context.canceled){
            IsRunning = false;
            walkSpeed = 5;
        }

    }
}
