using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("���ʰѼ�")]
    [SerializeField] private float runSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 3.0f;
    [SerializeField] private float climbSpeed = 4.0f;
    [SerializeField] private float doulbJumpSpeed = 8.0f;

    [Header("���D�Ѽ�")]
    [SerializeField] private float jumpSpeed = 6.0f;
   // [SerializeField] private float jumpHoldSpeed = 1.3f;
    [SerializeField] private float jumpHoldDuration = 0.1f;
   // [SerializeField] private float crouchJumpBoost = 2.5f;
    float jumpTime;

    [Header("���A")]
    public bool isCrouch;
    public bool isOnGround;
    public bool isJump;
    public bool isHeadBlocked;
    public bool isClimbing;
    public bool isOnLadder;
    private bool canDoubleJump;
    
    [Header("���Ҫ��A")]
    public float footOffset = 0.3f;   //�}��
    public float headClearance = 0.5f; //�Y��
    public float ladderDistance = 0.2f;
    public float groundDistance = 0.5f; //�a��
    public LayerMask groundLayer;
    public LayerMask ladderLayer;

    //�ץ�]�m
    bool jumpPressed;
   // bool jumpHeld;
    bool crouchHeld;
    bool climbHeld;


    // ����Ѽ�
    private Rigidbody2D rb; //����ե�
    private Animator animator; //�ʵe����
    private BoxCollider2D bcoll;  //Collider2d �O�����Collider�A BoxCollider2D �O����Collider
    private Animator myAnim;

    //�I����ؤo
    Vector2 collStandSize;
    Vector2 collStandOffset;
    Vector2 collCrouchSize;
    Vector2 collCrouchOffset;

    // ����
    Vector2 moveDirection = Vector2.zero;
    //float xVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        animator = GetComponent<Animator>();
        bcoll = GetComponent<BoxCollider2D>();
        myAnim = GetComponent<Animator>();

        collStandSize = bcoll.size;
        collStandOffset = bcoll.offset;      
        collCrouchSize = new Vector2(bcoll.size.x, bcoll.size.y / 2f);
        collCrouchOffset = new Vector2(bcoll.offset.x, bcoll.offset.y / 2f);
        
    }

    void Update()
    {
        jumpPressed = Input.GetButtonDown("Jump");
       // jumpHeld = Input.GetButton("Jump");
        crouchHeld = Input.GetButton("Crouch");
        climbHeld = Input.GetButtonDown("Climb");
       // UpdateAnimation();
    }

     void FixedUpdate()
    {
        //��bUpdate()�̪��~�|�ݰʡA���F�����ݰʿ��FixedUpdate()
        PhysicsCheck();
        runMove();
        MidAirMovement();
        Climb();
        SwitchAnimation();
    }

    //void UpdateAnimation()
    //{
    //    if (change != Vector3.zero)
    //    {
            
    //        animator.SetFloat("moveX", change.x);
    //     //  animator.SetFloat("moveY", change.y);
    //        animator.SetBool("moving", true);
    //    }
    //    else
    //    {
    //        animator.SetBool("moving", false);
    //    }
    //}

    //�P�_�O�_�b�a��
    void PhysicsCheck()
    {
        
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, -0.9f), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D righttCheck = Raycast(new Vector2(footOffset, -0.9f), Vector2.down, groundDistance, groundLayer);
        //bcoll.IsTouchingLayers(groundLayer)
        if (leftCheck||righttCheck)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
        //bcoll.size.y
        RaycastHit2D headCheck = Raycast(new Vector2(0f, 0.5f), Vector2.up, headClearance, groundLayer);
        if (headCheck)
        {
            isHeadBlocked = true;
        }
        else
        {
            isHeadBlocked = false;
        }

       // RaycastHit2D ladderCheek = Raycast(new Vector2(0f, 0f), Vector2.down,ladderDistance, ladderLayer);
        if (bcoll.IsTouchingLayers(ladderLayer))
        {
            isOnLadder = true;
            
        }
        else
        {
            isOnLadder = false;
            isClimbing = false;
        }

    }

    void runMove()
    {
        if (crouchHeld && !isCrouch && isOnGround)
        {
            Crouch();
        }else if (!crouchHeld && isCrouch &&!isHeadBlocked)
        {
            StandUp();
        }else if(!isOnGround && isCrouch)
        {
            StandUp();
        }
       // xVelocity = Input.GetAxisRaw("Horizontal");
        //rb.MovePosition( transform.position + change * runSpeed * Time.deltaTime );
        moveDirection.x = Input.GetAxisRaw("Horizontal");
       
        //�p�G�U�۪��A�t�״��
        if (isCrouch)
        {
            moveDirection.x /= crouchSpeed;
        }
        rb.velocity = new Vector2(moveDirection.x * runSpeed, rb.velocity.y);
        FaceDirction();
        bool plyerHasXAxisSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run", plyerHasXAxisSpeed);
    }

    void FaceDirction()
    {
        //�]�w���V��m
        if (moveDirection.x > 0.1f)
        {
            //facingRight = true;
            transform.localScale = new Vector2(1, 1);
        }
        else if (moveDirection.x < -0.1f)
        {
            //facingRight = false;
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void Crouch()
    {
        isCrouch = true;
        bcoll.size = collCrouchSize;
        bcoll.offset = collCrouchOffset;
    }

    void StandUp()
    {
        isCrouch = false;
        bcoll.size = collStandSize;
        bcoll.offset = collStandOffset;
    }
 
    void MidAirMovement()
    {
        //���D
        if (jumpPressed && isOnGround && !isJump)
        {
            if(isCrouch && isOnGround && !isHeadBlocked)
            {
                StandUp();
            }
            isOnGround = false;
            isJump = true;
            jumpTime = Time.time + jumpHoldDuration;
            rb.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
        
        }else if (isJump){

            if (canDoubleJump)
            {
                myAnim.SetBool("DoubleJump", true);
                Vector2 doubleJumpVel = new Vector2(0.0f, doulbJumpSpeed);
                rb.velocity = Vector2.up * doubleJumpVel;
                canDoubleJump = false;
            }
            // ��jumtime ���Ȥp��{�b�ɶ��ɡA���D����
            if (jumpTime < Time.time)
                {
                    isJump = false;
                }
        }

        //if (jumpHeld && isOnGround && !isJump)
        //{
        //    if(isCrouch && isOnGround)
        //    {
        //        StandUp();
        //        rb.AddForce(new Vector2(0f, crouchJumpBoost), ForceMode2D.Impulse);

        //    }
        //    isOnGround = false;
        //    isJump = true;

        //    //jumptime = �{�b�ɶ��[�]��o��
        //    jumpTime = Time.time + jumpHoldDuration;
        //    rb.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
        //}else if (isJump)
        //{
        //    //�����D
        //    if (jumpHeld)
        //    {
        //        rb.AddForce(new Vector2(0f, jumpHoldSpeed), ForceMode2D.Impulse);
        //    }

        //    // ��jumtime ���Ȥp��{�b�ɶ��ɡA���D����
        //    if (jumpTime < Time.time)
        //    {
        //        isJump = false;
        //    }
        //}
    }

    void Climb()
    {
        if (isOnLadder)
        {
            isClimbing = true;
            moveDirection.y = Input.GetAxisRaw("Vertical");
            //if (Mathf.Abs(moveDirection.y) > 0f)
            rb.velocity = new Vector2(rb.velocity.x, moveDirection.y * climbSpeed);
            rb.gravityScale = 0;
        }
        else if (!isOnLadder)
        {
            isClimbing = false;
            rb.gravityScale = 1;
        }
      
    }

    void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);
        if (myAnim.GetBool("Jump"))
        {
            if (rb.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }
        else if (isOnGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }

        if (myAnim.GetBool("DoubleJump"))
        {
            if (rb.velocity.y < 0.0f)
            {
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("DoubleFall", true);
            }
        }
        else if (isOnGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Idle", true);
        }
    }
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDiraction, float length, LayerMask layer)
    {  
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos+offset, rayDiraction, length, layer);
        Color color = hit ? Color.yellow : Color.red;
        Debug.DrawRay(pos + offset, rayDiraction * length, color);
        return hit;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ladder"))
    //    {
    //        isOnLadder = true;
            
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ladder"))
    //    {
    //        isOnLadder = false;
    //        isClimbing = false;
    //    }
    //}
}

