using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float AirWalkSpeed = 5f;
    public float jumpImpulse = 10f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;

   
    public float CurrentMoveSpeed{ get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }

                    }
                    else
                    {
                        return AirWalkSpeed;
                    }
                }
                else
                {
                    //idle speed
                    return 0;
                }
            }
            else
            {
                return 0;
            }
            
        }
    }


    private bool _isMoving = false;

    public bool IsMoving { get
        {
             return _isMoving;
        }
        private set
        {
             _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool("isRunning", value );
        }
    }




    public bool _isFacingRight = true;

    public bool IsFacingRight{get{return _isFacingRight;} private set{
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }


    public bool CanMove {
        get
        {
            return animator.GetBool("canMove");
        }
       
    }
    
    Rigidbody2D rb;
    Animator animator;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat("yVelocity", rb.velocity.y);

    }


    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);


    }


    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRunning = true; 
        }
        else if(context.canceled)
        {
            IsRunning = false;
        }
    }


    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x> 0 && !IsFacingRight)
        {
            //face the right
            IsFacingRight = true;

        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            //face the left
            IsFacingRight= false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // todo check if alive as well
        if(context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }


    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("attack");
        }
    }

}
