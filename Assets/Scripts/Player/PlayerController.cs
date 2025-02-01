using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private Animator animator;

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }

        private set
        {
            _isMoving = value;

            animator.SetBool(AnimationStrings.isMoving, _isMoving);
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

        private set
        {
            _isRunning = value;

            animator.SetBool(AnimationStrings.isRunning, _isRunning);
        }
    }

    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirectionsScript.IsOnWall)
                {
                    if (touchingDirectionsScript.IsGrounded)
                    {
                        if (_isRunning)
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
                        // Air State
                        return airWalkSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }

    private Vector2 moveInput;

    private Rigidbody2D rb;

    private bool _isFacingRight = true;

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }

        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    public float jumpSpeed = 10f;
    private TouchingDirections touchingDirectionsScript;

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    Damageable damageableScript;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirectionsScript = GetComponent<TouchingDirections>();
        damageableScript = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if (!damageableScript.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = (moveInput != Vector2.zero);
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }


    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            // Face Right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // Face Left
            IsFacingRight = false;
        }
    }

    

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirectionsScript.IsGrounded && CanMove)
        {
            SoundManager.instance.PlaySound(jumpSound);
            animator.SetTrigger(AnimationStrings.jumpTrigger);

            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    

    
}
