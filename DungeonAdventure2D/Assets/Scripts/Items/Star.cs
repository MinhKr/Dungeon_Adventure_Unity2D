using UnityEngine;

public class Star : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] GameObject pickupVfx;

    [SerializeField] private float speed = .75f;
    [SerializeField] private float travelDistance = 1f;
    public Vector3[] wayPoints;

    private int wayPointIndex;

    private void Start()
    {
        gameManager = GameManager.instance;
        SetUpwayPoints();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            AudioManager.instance.PlaySFX(8);
            Destroy(gameObject);
            GameObject newPickupVfx = Instantiate(pickupVfx, transform.position, Quaternion.identity);
        }
    }

    private void SetUpwayPoints()
    {
        wayPoints = new Vector3[2];

        float yOffset = travelDistance / 2;

        wayPoints[0] = transform.position + new Vector3(0, yOffset, 0);
        wayPoints[1] = transform.position + new Vector3(0, -yOffset, 0);
    }

    private void HandleMovement()
    {
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
}
