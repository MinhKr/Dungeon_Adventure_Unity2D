using UnityEngine;

public class EnemyMushroom : Enemy
{
    protected override void Update()
    {
        base.Update();

        if (isDead)
            return;

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
}
