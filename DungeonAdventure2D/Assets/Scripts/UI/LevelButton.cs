using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNumberText;
    private int levelIndex;
    private string sceneName;

    public void SetUpLevelButton(int newLevelIndex)
    {
        levelIndex = newLevelIndex;
        levelNumberText.text = levelIndex.ToString();
        sceneName = "Level " + levelIndex.ToString();    
    }

    public void LoadLevel()
    {
        AudioManager.instance.PlaySFX(4);
        SceneManager.LoadScene(sceneName);
    }
}
