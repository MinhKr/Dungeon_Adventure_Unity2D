using UnityEngine;

public class Deadzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            UIingame.instance.HealthbarCurrent.uvRect = new Rect(10 / 10f, 0, 1, 1);
            GameManager.instance.Die();
        }
    }
}
