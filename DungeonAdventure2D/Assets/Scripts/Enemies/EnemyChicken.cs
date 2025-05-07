using UnityEngine;

public class EnemyChicken : Enemy
{
    [Header("Chicken Properties")]
    [SerializeField] private float agroDuration;
    private float agroTimer;
    private bool canFlip = true;

    protected override void Update()
    {
        base.Update();

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
}
