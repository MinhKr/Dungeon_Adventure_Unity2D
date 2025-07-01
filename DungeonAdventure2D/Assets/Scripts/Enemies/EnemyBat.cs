using UnityEngine;

public class EnemyBat : Enemy
{
    private Vector3 originPosition;
    private Vector3 destination;

    [SerializeField] private bool canDetectPlayer;

    [SerializeField] private Collider2D targetCollider;
    [SerializeField] private float attackRange = 1.5f;

    protected override void Awake()
    {
        base.Awake();

        this.originPosition = transform.position;

        canMove = false;
    }

    protected override void Update()
    {
        base.Update();

        if(idleTimer < 0)
            canDetectPlayer = true;

        HandleMovement();
        HandleDetectPlayer();
    }

    private void HandleMovement()
    {
        if (canMove == false)
            return;

        HandleFlip(destination.x);
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, destination) < .1f)
        {
            if(destination == originPosition)
            {
                idleTimer = idleTime;
                canDetectPlayer = false;
                targetCollider = null;
                canMove = false;
                anim.SetBool("isFlying", false);
            }
            else
            {
                destination = originPosition;
            }
        }
    }

    private void HandleDetectPlayer()
    {
        if (targetCollider == null && canDetectPlayer)
        {
            /*targetCollider = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);

            if (targetCollider != null)
            {
                canDetectPlayer = false;
                destination = targetCollider.transform.position;
                anim.SetBool("isFlying", true);
            }*/
            Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);

            if (hit != null)
            {
                // calculate the direction to the player
                Vector2 toPlayer = (hit.transform.position - transform.position).normalized;

                // angle between the down vector (Vector2.down) and the direction to the player
                float angle = Vector2.Angle(Vector2.down, toPlayer);

                // detect player only if the angle is less than 90 degrees
                if (angle < 90f)
                {
                    targetCollider = hit;
                    canDetectPlayer = false;
                    destination = hit.transform.position;
                    anim.SetBool("isFlying", true);
                }
            }
        }
    }
    public void AllowToFly() => canMove = true;

    protected override void HandleAnimation()
    {

    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
