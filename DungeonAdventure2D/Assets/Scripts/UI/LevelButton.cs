using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNumberText;
    private int levelIndex;
    public string sceneName;

    public void SetUpLevelButton(int newLevelIndex)
    {
        levelIndex = newLevelIndex;
        levelNumberText.text = levelIndex.ToString();
        sceneName = "Level " + levelIndex.ToString();    
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }
}
