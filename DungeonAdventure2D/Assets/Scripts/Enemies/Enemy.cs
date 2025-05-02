using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;

    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float idleTime = 1.5f;
    protected float idleTimer;

    [Header("Death Properties")]
    [SerializeField] protected float deathImpact = 5f ;
    [SerializeField] protected float deathRotationSpeed = 150f;
    protected float deathRotationAngle = 1;
    protected bool isDead;
    [Space]
    [SerializeField] protected GameObject damageTrigger;

    [Header("Collision Properties")]
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform groundCheck;
    protected bool isGrounded;
    protected bool isWallDetected;

    protected int facingDirection = -1;
    protected bool facingRight = false;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        idleTimer -= Time.deltaTime;

        if (isDead)
            HandleDeathRotation();
    }

    //Death
    public virtual void Die()
    {
        isDead = true;  
        anim.SetTrigger("hit");
        damageTrigger.SetActive(false);
        rb.linearVelocity = new Vector2(rb.linearVelocityX, deathImpact);

        if (Random.Range(0, 100) < 50)
            deathRotationAngle = deathRotationAngle * -1;
    }

    private void HandleDeathRotation()
    {
        transform.Rotate(0.0f, 0.0f, (deathRotationSpeed * deathRotationAngle) * Time.deltaTime);
    }

    //Flip
    protected virtual void HandleFlip(float xValue)
    {
        if (facingRight && xValue < 0 || !facingRight && xValue > 0)
            Flip();
    }
    protected virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
        facingRight = !facingRight;
    }

    //Collision
    protected virtual void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
    }
}
