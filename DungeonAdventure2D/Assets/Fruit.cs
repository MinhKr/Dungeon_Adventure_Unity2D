using UnityEngine;

public class Fruit : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] GameObject pickupVFX;

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

            GameObject newVfx = Instantiate(pickupVFX, transform.position, Quaternion.identity);

            Destroy(newVfx, .5f);
        }
    }
}
