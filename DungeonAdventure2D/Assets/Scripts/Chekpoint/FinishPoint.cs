using System.Collections;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();
    private float timeDelay = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if(player != null)
        {
            anim.SetTrigger("activate");
            StartCoroutine(ShowCompleteUICouroutine());
        }
    }
    private IEnumerator ShowCompleteUICouroutine()
    {
        yield return new WaitForSeconds(timeDelay);

        UIingame.instance.ShowCompletedUI(GameManager.instance.fruitCollected);
    }

}
