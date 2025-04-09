using UnityEngine;

public class Fruit : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] GameObject pickupVfx;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            gameManager.AddFruit();
            Destroy(gameObject);
            GameObject newPickupVfx = Instantiate(pickupVfx, transform.position, Quaternion.identity);
        }
    }
}
