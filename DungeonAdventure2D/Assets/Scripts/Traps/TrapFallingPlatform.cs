using UnityEngine;

public class TrapFallingPlatform : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private Collider2D[] colliders;

    [SerializeField] private float speed = .75f;
    [SerializeField] private float travelDistance = 1f;
    public Vector3[] wayPoints;

    private bool canMove = false;
    private int wayPointIndex;

    [Header("Falling Properties")]  
    [SerializeField] private float fallDelay;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        colliders = GetComponents<Collider2D>();
    }

    private void Start()
    {
        SetUpwayPoints();

        float randomDelay = Random.Range(0, .6f);    
        Invoke(nameof(ActivatePlatform), randomDelay);
    }

    private void ActivatePlatform() => canMove = true;

    private void SetUpwayPoints()
    {
        wayPoints = new Vector3[2];

        float yOffset = travelDistance / 2;

        wayPoints[0] = transform.position + new Vector3(0, yOffset, 0);
        wayPoints[1] = transform.position + new Vector3(0, -yOffset, 0);
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (canMove == false)
            return;

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[wayPointIndex], speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wayPoints[wayPointIndex]) < .1f)
        {
            wayPointIndex++;

            if (wayPointIndex >= wayPoints.Length)
            {
                wayPointIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            Invoke(nameof(SwitchOffPlatform), fallDelay);
        }
    }

    private void SwitchOffPlatform()
    {
        anim.SetTrigger("deactivate");
        canMove = false;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1.5f;

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
