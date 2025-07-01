using UnityEngine;

public class TrapSpikedHead : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform[] wayPoint;
    [SerializeField] private float delayTime;

    private int moveDirection = 1;

    public int wayPointIndex = 1;

    private void Start()
    {
        /*transform.position = wayPoint[0].position;*/
        float t = Random.Range(0f, 1f);

        transform.position = Vector2.Lerp(wayPoint[0].position, wayPoint[1].position, t);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoint[wayPointIndex].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wayPoint[wayPointIndex].position) < 0.1f)
        {
            if (wayPointIndex == 0 || wayPointIndex == (wayPoint.Length - 1))
            {
                moveDirection = moveDirection * -1;
            }

            wayPointIndex = wayPointIndex + moveDirection;
        }
    }
}
