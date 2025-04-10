using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();
    private bool isActive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isActive) return; 

        Player player = collision.GetComponent<Player>();

        if(player != null)
        {
            ActivateCheckPoint();
        }
    }

    private void ActivateCheckPoint()
    {
        isActive = true;
        GameManager.instance.UpdateRespawnPoint(transform);
        anim.SetTrigger("activate");
    }
}
