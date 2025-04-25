using UnityEngine;

public class TrapSpikedBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D spikedBallRb;
    [SerializeField] private float pushForce;

    private void Start()
    {
        Vector2 pushVector = new Vector2(pushForce, 0);

        spikedBallRb.AddForce(pushVector, ForceMode2D.Impulse);
    }
}
