using UnityEngine;

public class EnemyMushroom : Enemy
{
    protected override void Update()
    {
        base.Update();

        HandleCollision();

        if (isWallDetected || !isGrounded)
        {
            Flip();
            idleTimer = idleTime;
            rb.linearVelocity = Vector2.zero;
        }

        anim.SetFloat("xVelocity", rb.linearVelocityX);

        HandleMovement();
    }

    private void HandleMovement()
    {
        if (idleTimer > 0)
            return;

        if (isGrounded)
            rb.linearVelocity = new Vector2(moveSpeed * facingDirection, rb.linearVelocityY);
    }
}
