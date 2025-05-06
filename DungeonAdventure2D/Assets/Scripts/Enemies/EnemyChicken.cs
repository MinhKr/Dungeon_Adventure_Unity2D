using UnityEngine;

public class EnemyChicken : Enemy
{
    [Header("Chicken Properties")]
    [SerializeField] private float detectionRange;
    [SerializeField] private float agroDuration;
    private float agroTimer;
    private bool playerDetected;
    private bool canFlip = true;

    protected override void Update()
    {
        base.Update();
        anim.SetFloat("xVelocity", rb.linearVelocityX);

        if (isDead)
            return;

        agroTimer -= Time.deltaTime;

        if (playerDetected)
        {
            canMove = true;
            agroTimer = agroDuration;
        }

        if (agroTimer <= 0)
            canMove = false;

        HandleCollision();

        HandleMovement();

        HandleTurnAround();
    }

    private void HandleTurnAround()
    {
        if (isWallDetected || !isGrounded)
        {
            Flip();
            canMove = false;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void HandleMovement()
    {
        if (canMove == false)
            return;

        HandleFlip(player.position.x);

        if (isGrounded)
            rb.linearVelocity = new Vector2(moveSpeed * facingDirection, rb.linearVelocityY);
    }

    protected override void HandleFlip(float xValue)
    {
        if (facingRight && xValue < transform.position.x || !facingRight && xValue > transform.position.x)
        {
            if (canFlip)
            {
                canFlip = false;
                Invoke(nameof(Flip), .3f);
            }
        }
    }

    protected override void Flip()
    {
        base.Flip();
        canFlip = true;
    }

    protected override void HandleCollision()
    {
        base.HandleCollision();

        playerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, detectionRange, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + detectionRange * facingDirection, transform.position.y));
    }
}
