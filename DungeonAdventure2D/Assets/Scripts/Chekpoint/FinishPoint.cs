using System.Collections;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();
    private float timeDelay = 0.5f;

    [SerializeField] private int levelIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if(player != null)
        {
            AudioManager.instance.PlaySFX(2);
            anim.SetTrigger("activate");
            StartCoroutine(ShowCompleteUICouroutine());
        }
    }
    private IEnumerator ShowCompleteUICouroutine()
    {
        yield return new WaitForSeconds(timeDelay);

        SaveStars(GameManager.instance.starCollected);
        UnlockNextLevel();

        UIingame.instance.ShowCompletedUI(GameManager.instance.fruitCollected);
    }

    private void SaveStars(int collectedStars)
    {
        string key = "Level" + levelIndex + "_Star";
        int savedStars = PlayerPrefs.GetInt(key, 0);

        if (collectedStars > savedStars)
        {
            PlayerPrefs.SetInt(key, collectedStars);
            PlayerPrefs.Save();
        }
    }

    private void UnlockNextLevel()
    {
        int nextLevel = levelIndex + 1;
        string nextKey = "Level" + nextLevel + "_Unlocked";

        PlayerPrefs.SetInt(nextKey, 1);
        PlayerPrefs.Save();
    }
}
