using UnityEngine;

public class EnemyTrunk : Enemy
{
    [Header("Trunk Properties")]
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float lastTimeAttacked;

    [Header("Bullet")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 10f;
    override protected void Update()
    {
        base.Update();

        if (isDead)
            return;

        bool canAttack = Time.time > lastTimeAttacked + attackCooldown;

        if (playerDetected && canAttack)
            Attack();

        HandleTurnAround();
        HandleMovement();
    }
    private void Attack()
    {
        rb.linearVelocity = Vector2.zero;
        idleTimer = idleTime;
        lastTimeAttacked = Time.time;
        anim.SetTrigger("attack");
    }

    public void CreateBullet()
    {
        Bullet newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        Vector2 bulletVelocity = new Vector2(facingDirection * bulletSpeed, 0);
        newBullet.SetVelocity(bulletVelocity);

        if (facingDirection == 1)
            newBullet.FlipBullet();

        Destroy(newBullet.gameObject, 5);
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
