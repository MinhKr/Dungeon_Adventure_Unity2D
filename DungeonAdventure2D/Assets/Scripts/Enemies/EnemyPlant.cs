using UnityEngine;

public class EnemyPlant : Enemy
{
    [Header("Plant Properties")]
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float lastTimeAttacked;

    [Header("Bullet")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 10f;
    protected override void Update()
    {
        base.Update();

        bool canAttack = Time.time > lastTimeAttacked + attackCooldown;

        if (playerDetected && canAttack)
            Attack();
    }
    
    private void Attack()
    {
        lastTimeAttacked = Time.time;
        anim.SetTrigger("attack");
    }

    public void CreateBullet()
    {
        Bullet newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);  

        Vector2 bulletVelocity = new Vector2(facingDirection * bulletSpeed, 0);
        newBullet.SetVelocity(bulletVelocity);
        Destroy(newBullet.gameObject, 5);
    }

    protected override void HandleAnimation()
    {
        //empty
    }
}
