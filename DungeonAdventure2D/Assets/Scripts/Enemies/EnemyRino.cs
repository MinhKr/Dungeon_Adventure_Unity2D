using UnityEngine;

public class EnemyRino : Enemy
{
    [Header("Rino Properties")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedIncreaseRate = 1.5f;
    private float defaultSpeed;
    [SerializeField] private Vector2 wallHitKnockback;

    protected override void Start()
    {
        base.Start();

        defaultSpeed = moveSpeed;
    }

    protected override void Update()
    {
        base.Update();

        if (playerDetected)
            canMove = true;

        HandleSpeed();

        HandleMovement();
    }

    private void HandleSpeed()
    {
        moveSpeed += Time.deltaTime * speedIncreaseRate;

        if (moveSpeed > maxSpeed)
            moveSpeed = maxSpeed;
    }

    private void HandleMovement()
    {
        if (canMove == false)
            return;

        rb.linearVelocity = new Vector2(moveSpeed * facingDirection, rb.linearVelocityY);

        if (isWallDetected)
            WallHit();

        if (!isGrounded)
        {
            moveSpeed = defaultSpeed;
            canMove = false;
            rb.linearVelocity = Vector2.zero;
            Flip();
        }
    }

    private void WallHit()
    {
        moveSpeed = defaultSpeed;
        canMove = false;
        anim.SetBool("hitWall", true);
        rb.linearVelocity = new Vector2(wallHitKnockback.x * -facingDirection, wallHitKnockback.y);
    }

    public void WallHitOver()
    {
        anim.SetBool("hitWall", false);
        Invoke(nameof(Flip), .7f);
    }
}
