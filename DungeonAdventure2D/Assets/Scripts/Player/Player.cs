using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private bool canBeControlled = false;

    [Header("Movement Properties")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    private float defaultGravityScale;

    private bool canDoubleJump;

    [Header("Wall Jump Properties")]
    [SerializeField] private float wallJumpDuration;
    [SerializeField] private Vector2 wallJumpForce;
    private bool isWallJumping;

    [Header("KnockBack")]
    [SerializeField] private Vector2 knockBackPower;
    [SerializeField] private float knockBackDuration;
    private bool isKnocked;
    private bool canBeKnocked;

    [Header("Collision Properties")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;
    private bool isWallDetected;
    [Space]
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private Transform enemyCheck;
    [SerializeField] private float enemyCheckRadius;

    private float xInput;
    private float yInput;

    private bool facingRight = true;
    private int facingDirection = 1;

    [Header("VFX")]
    [SerializeField] private GameObject deathVfx;

    [Header("Player Visuals")]
    [SerializeField] private AnimatorOverrideController[] animators;
    [SerializeField] private int skinId;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        defaultGravityScale = rb.gravityScale;
        RespawnFinished(false);

        ChooseSkin(SkinManager.instance.skinIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeControlled == false)
        {
            HandleCollision();
            HandleAnimations();
            return;
        }

        if (isKnocked)
            return;

        HandleEnemyCollision();

        HandleInput();

        HandleWallSlide();

        HandleMovement();

        HandleFlip();

        HandleCollision();

        HandleAnimations();
    }

    private void HandleEnemyCollision()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius, whatIsEnemy);

        foreach (var enemy in enemies)
        {
            Enemy newEnemy = enemy.GetComponent<Enemy>();
            if (newEnemy != null)
            {
                newEnemy.Die();
                Jump();
            }
        }
    }

    public void RespawnFinished(bool finished)
    {
        if (finished)
        {
            rb.gravityScale = defaultGravityScale;
            canBeControlled = true;
        }
        else
        {
            rb.gravityScale = 0;
            canBeControlled = false;
        }
    }

    //Knockback
    public void KnockBack(float sourceDamageXPosition)
    {
        float knockDir = 1;
        if (transform.position.x < sourceDamageXPosition)
            knockDir = -1;

        if (isKnocked)
            return;

        StartCoroutine(KnockBackCooldown());

        rb.linearVelocity = new Vector2(knockBackPower.x * knockDir, knockBackPower.y);
    }
    private IEnumerator KnockBackCooldown()
    {
        isKnocked = true;
        anim.SetBool("isKnocked", isKnocked);

        yield return new WaitForSeconds(knockBackDuration);

        isKnocked = false;
        anim.SetBool("isKnocked", isKnocked);
    }

    //Push
    public void Push(Vector2 pushDirection, float duration)
    {
        StartCoroutine(PushCoroutine(pushDirection, duration));
    }

    private IEnumerator PushCoroutine(Vector2 pushDirection, float duration)
    {
        canBeControlled = false;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(pushDirection, ForceMode2D.Impulse);

        yield return new WaitForSeconds(duration);

        canBeControlled = true;
    }

    public void Die()
    {
        Destroy(gameObject);
        GameObject newDeathVfx = Instantiate(deathVfx, transform.position, Quaternion.identity);
    }
    private void HandleWallSlide()
    {
        bool canWallSlide = isWallDetected && rb.linearVelocity.y < 0;
        float yModifier = yInput < 0 ? 1 : .5f;

        if (canWallSlide == false)
            return;

        rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * yModifier);
    }
    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
        }
    }

    //Jumping
    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    private void DoubleJump()
    {
        isWallJumping = false;
        canDoubleJump = false;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, doubleJumpForce);
    }
    private void WallJump()
    {
        canDoubleJump = true;

        rb.linearVelocity = new Vector2(wallJumpForce.x * -facingDirection, wallJumpForce.y);

        Flip();

        StopAllCoroutines();
        StartCoroutine(WallJumpCooldown());
    }
    private IEnumerator WallJumpCooldown()
    {
        isWallJumping = true;

        yield return new WaitForSeconds(wallJumpDuration);

        isWallJumping = false;
    }
    private void JumpButton()
    {
        if (isGrounded)
        {
            canDoubleJump = true;
            Jump();
        }
        else if (isWallDetected && !isGrounded)
            WallJump();
        else if (canDoubleJump)
            DoubleJump();
    }

    //Movement
    private void HandleMovement()
    {
        if (isWallDetected)
            return;

        if (isWallJumping)
            return;

        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocityY);
    }

    //Animation
    private void HandleAnimations()
    {
        anim.SetFloat("xVelocity", rb.linearVelocityX);
        anim.SetFloat("yVelocity", rb.linearVelocityY);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWallDetected", isWallDetected);
    }

    public void ChooseSkin(int skinIndex)
    {
        anim.runtimeAnimatorController = animators[skinIndex];
    }

    //Flip
    private void HandleFlip()
    {
        if (facingRight && xInput < 0 || !facingRight && xInput > 0)
            Flip();
    }
    private void Flip()
    {
        facingDirection = facingDirection * -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
        facingRight = !facingRight;
    }

    //Collision
    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadius);
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
    }
}
