using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform[] wayPoint;

    public int wayPointIndex = 0;
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoint[wayPointIndex].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wayPoint[wayPointIndex].position) < 0.1f)
        {
            if (wayPointIndex == (wayPoint.Length - 1))
            {
                wayPointIndex = wayPointIndex * -1;
            }

            wayPointIndex = wayPointIndex + 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.transform.SetParent(null);
        }
    }
}
