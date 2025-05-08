using UnityEngine;

public class Bullet : MonoBehaviour
{
    private string playerLayer = "Player";
    private string groundLayer = "Ground";
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void FlipBullet() => sr.flipX = !sr.flipX;

    public void SetVelocity(Vector2 velocity) => rb.linearVelocity = velocity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(playerLayer))
        {
            collision.gameObject.GetComponent<Player>().KnockBack(transform.position.x);
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer(groundLayer))
        {
            Destroy(gameObject);
        }
    }
}
