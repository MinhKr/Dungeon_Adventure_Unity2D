using UnityEngine;

public class EnemyMushroom : Enemy
{
    private BoxCollider2D cd;

    override protected void Awake()
    {
        base.Awake();
        cd = GetComponent<BoxCollider2D>();
    }
    protected override void Update()
    {
        base.Update();
        anim.SetFloat("xVelocity", rb.linearVelocityX);

        if (isDead)
            return;

        HandleCollision();

        HandleMovement();

        HandleTurnAround();
    }

    private void HandleTurnAround()
    {
        if (isWallDetected || !isGrounded)
        {
            Flip();
            idleTimer = idleTime;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void HandleMovement()
    {
        if (idleTimer > 0)
            return;

        if (isGrounded)
            rb.linearVelocity = new Vector2(moveSpeed * facingDirection, rb.linearVelocityY);
    }
    public override void Die()
    {
        base.Die();
        cd.enabled = false;
    }
}
