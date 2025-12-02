using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    [Header("Hareket Ayarlari")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float acceleration = 50f;
    public float deceleration = 50f;
    public float airControl = 0.5f;

    [Header("Z�plama Ayarlari")] 
    public float jumpBufferTime = 0.2f; 

    [Header("Zemin Kontrol�")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayerMask;
    public int extraGroundChecks = 2; 

    [Header("Duvar Kontrol�")]
    public Transform wallCheckLeft;
    public Transform wallCheckRight;
    public float wallCheckDistance = 0.1f;
    public LayerMask wallLayerMask;

    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private Animator animator;
    private bool isGrounded;
    private bool wasGrounded;
    private float jumpBufferCounter;
    private float horizontalInput;
    private bool jumpInput;
    private bool jumpInputDown;
    private bool isOnWallLeft;
    private bool isOnWallRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        rb.linearDamping = 0f;

        if (col == null)
        {
            col = gameObject.AddComponent<CapsuleCollider2D>();
        }

        CreatePhysicsMaterial();

        SetupCheckPoints();
    }

    void CreatePhysicsMaterial()
    {
        PhysicsMaterial2D playerMaterial = new PhysicsMaterial2D("PlayerMaterial");
        playerMaterial.friction = 0f; 
        playerMaterial.bounciness = 0f; 
        col.sharedMaterial = playerMaterial;
    }

    void SetupCheckPoints()
    {
        if (groundCheck == null)
        {
            GameObject groundCheckObj = new GameObject("GroundCheck");
            groundCheckObj.transform.SetParent(transform);
            groundCheckObj.transform.localPosition = new Vector3(0, -col.size.y * 0.5f - groundCheckRadius, 0);
            groundCheck = groundCheckObj.transform;
        }

        if (wallCheckLeft == null)
        {
            GameObject wallCheckLeftObj = new GameObject("WallCheckLeft");
            wallCheckLeftObj.transform.SetParent(transform);
            wallCheckLeftObj.transform.localPosition = new Vector3(-col.size.x * 0.5f - wallCheckDistance, 0, 0);
            wallCheckLeft = wallCheckLeftObj.transform;
        }

        if (wallCheckRight == null)
        {
            GameObject wallCheckRightObj = new GameObject("WallCheckRight");
            wallCheckRightObj.transform.SetParent(transform);
            wallCheckRightObj.transform.localPosition = new Vector3(col.size.x * 0.5f + wallCheckDistance, 0, 0);
            wallCheckRight = wallCheckRightObj.transform;
        }
    }

    void Update()
    {
        GetInputs();
        CheckGround();
        CheckWalls();

        if (jumpInputDown)
        {
            jumpBufferCounter = jumpBufferTime;
        }

        HandleJumpBuffer();

        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;

        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving);
        }
    }

    void FixedUpdate()
    {
        if (rb.bodyType == RigidbodyType2D.Static)
        {
            return;
        }
        
        HandleMovement();
        HandleJump();
    }

    void GetInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetButton("Jump");
        jumpInputDown = Input.GetButtonDown("Jump");
    }

    void CheckGround()
    {
        wasGrounded = isGrounded;

        bool groundHit = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);

        bool leftGroundHit = false;
        bool rightGroundHit = false;

        if (extraGroundChecks > 0)
        {
            float colliderWidth = col.size.x * 0.4f;
            Vector2 leftCheck = groundCheck.position - Vector3.right * colliderWidth;
            Vector2 rightCheck = groundCheck.position + Vector3.right * colliderWidth;

            leftGroundHit = Physics2D.OverlapCircle(leftCheck, groundCheckRadius * 0.8f, groundLayerMask);
            rightGroundHit = Physics2D.OverlapCircle(rightCheck, groundCheckRadius * 0.8f, groundLayerMask);
        }

        isGrounded = groundHit || leftGroundHit || rightGroundHit;
    }

    void CheckWalls()
    {
        isOnWallLeft = Physics2D.OverlapCircle(wallCheckLeft.position, wallCheckDistance, wallLayerMask);
        isOnWallRight = Physics2D.OverlapCircle(wallCheckRight.position, wallCheckDistance, wallLayerMask);
    }

    void HandleJumpBuffer()
    {
        if (jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    void HandleMovement()
    {
        float targetSpeed = horizontalInput * moveSpeed;
        float currentSpeed = rb.linearVelocity.x;

        float controlFactor = isGrounded ? 1f : airControl;

        float speedDifference = targetSpeed - currentSpeed;
        float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        accelerationRate *= controlFactor;

        bool hitWall = (isOnWallLeft && horizontalInput < 0) || (isOnWallRight && horizontalInput > 0);
        if (hitWall && isGrounded)
        {
            targetSpeed = 0f;
            speedDifference = -currentSpeed;
        }

        float movement = speedDifference * accelerationRate * Time.fixedDeltaTime;
        rb.linearVelocity = new Vector2(currentSpeed + movement, rb.linearVelocity.y);
    }

    void HandleJump()
    {
        bool canJump = (isGrounded && jumpBufferCounter > 0f);

        if (canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpBufferCounter = 0f;
        }

        if (!jumpInput && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    public bool IsOnWall()
    {
        return isOnWallLeft || isOnWallRight;
    }

    public float GetHorizontalInput()
    {
        return horizontalInput;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        if (extraGroundChecks > 0 && col != null)
        {
            float colliderWidth = col.size.x * 0.4f;
            Vector2 leftCheck = groundCheck.position - Vector3.right * colliderWidth;
            Vector2 rightCheck = groundCheck.position + Vector3.right * colliderWidth;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(leftCheck, groundCheckRadius * 0.8f);
            Gizmos.DrawWireSphere(rightCheck, groundCheckRadius * 0.8f);
        }

        if (wallCheckLeft != null && wallCheckRight != null)
        {
            Gizmos.color = isOnWallLeft ? Color.red : Color.blue;
            Gizmos.DrawWireSphere(wallCheckLeft.position, wallCheckDistance);

            Gizmos.color = isOnWallRight ? Color.red : Color.blue;
            Gizmos.DrawWireSphere(wallCheckRight.position, wallCheckDistance);
        }
    }
}
