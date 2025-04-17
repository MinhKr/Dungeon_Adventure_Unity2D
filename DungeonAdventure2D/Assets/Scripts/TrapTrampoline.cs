using UnityEngine;

public class TrapTrampoline : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float pushForce;
    [SerializeField] private float duration = .5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.Push(transform.up * pushForce, duration);
            anim.SetTrigger("activate");
        }
    }
}
