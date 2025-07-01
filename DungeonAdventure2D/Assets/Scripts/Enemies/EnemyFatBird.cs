using UnityEngine;

public class EnemyFatBird : Enemy
{
    private Vector3 originalPosition;
    private bool isFalling = false;
    private bool isReturning = false;

    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private float returnSpeed = 2f;

    protected override void Awake()
    {
        base.Awake();
        originalPosition = transform.position;
    }

    protected override void Update()
    {
        base.Update();

        if (!isFalling && !isReturning && playerDetected)
        {
            isFalling = true;
        }

        if (isFalling)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            

            if (isGrounded)
            {
                isFalling = false;
                isReturning = true;
            }
        }
        else if (isReturning)
        {
            if(isDead)
                return;

            transform.position = Vector3.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, originalPosition) < 0.1f)
            {
                isReturning = false;
            }
        }
    }

    protected override void HandleCollision()
    {
        base.HandleCollision();

        if (!isFalling && !isReturning)
            playerDetected = Physics2D.Raycast(transform.position, Vector2.down, detectionRange, whatIsPlayer);
    }

    protected override void HandleAnimation()
    {
        anim.SetBool("isfalling", isFalling);
        anim.SetBool("isgrounded", isGrounded);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * detectionRange);
    }
}
