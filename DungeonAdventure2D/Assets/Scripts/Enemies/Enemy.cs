using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float idleTime;
    protected float idleTimer;

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
