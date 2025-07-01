using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected Collider2D[] col;
    protected SpriteRenderer sr => GetComponent<SpriteRenderer>();

    [Header("General Properties")]
    [SerializeField] protected Transform player;
    [Space]
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float idleTime = 1.5f;
    protected float idleTimer;
    protected bool canMove;

    [Header("Death Properties")]
    [SerializeField] protected float deathImpact = 5f;
    [SerializeField] protected float deathRotationSpeed = 150f;
    protected float deathRotationAngle = 1;
    protected bool isDead;

    [Header("Collision Properties")]
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected float detectionRange;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected Transform groundCheck;
    protected bool isGrounded;
    protected bool isWallDetected;
    protected bool playerDetected;

    protected int facingDirection = -1;
    protected bool facingRight = false;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponentsInChildren<Collider2D>();
    }

    protected virtual void Start()
    {
        InvokeRepeating(nameof(UpdatePlayerRef), 0, 1);

        if (sr.flipX == true && !facingRight)
        {
            Flip();
            sr.flipX = false;
        }

    }

    protected virtual void Update()
    {
        HandleCollision();
        HandleAnimation();

        idleTimer -= Time.deltaTime;

        if (isDead)
            HandleDeathRotation();
    }

    private void UpdatePlayerRef()
    {
        if (player == null)
            player = GameManager.instance.player.transform;
    }

    protected virtual void HandleAnimation()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
    }

    //Death
    public virtual void Die()
    {
        isDead = true;
        if(rb.bodyType == RigidbodyType2D.Kinematic)
            rb.bodyType = RigidbodyType2D.Dynamic;
        foreach (var collider in col)
            collider.enabled = false;

        anim.SetTrigger("hit");

        rb.linearVelocity = new Vector2(rb.linearVelocityX, deathImpact);

        if (Random.Range(0, 100) < 50)
            deathRotationAngle = deathRotationAngle * -1;

        canMove = false;
    }

    private void HandleDeathRotation()
    {
        transform.Rotate(0.0f, 0.0f, (deathRotationSpeed * deathRotationAngle) * Time.deltaTime);
    }

    //Flip
    protected virtual void HandleFlip(float xValue)
    {
        if (facingRight && xValue < transform.position.x || !facingRight && xValue > transform.position.x)
            Flip();
    }
    protected virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
        facingRight = !facingRight;
    }

    [ContextMenu("Change Facing Direction")]
    public void FacingDirectionDefault()
    {
        sr.flipX = !sr.flipX;
    }

    //Collision
    protected virtual void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
        playerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, detectionRange, whatIsPlayer);
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + detectionRange * facingDirection, transform.position.y));
    }
}
