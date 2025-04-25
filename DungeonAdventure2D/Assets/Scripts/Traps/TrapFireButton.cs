using UnityEngine;

public class TrapFireButton : MonoBehaviour
{
    private TrapFire trapFire;
    private Animator anim;

    private void Awake()
    {
        trapFire = GetComponentInParent<TrapFire>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            trapFire.SwitchOffFire();
            anim.SetTrigger("activate");
        }
    }
}
