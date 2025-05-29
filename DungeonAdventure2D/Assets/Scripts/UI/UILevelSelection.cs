using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevelSelection : MonoBehaviour
{
    [SerializeField] private LevelButton levelButtonPrefab;
    [SerializeField] private Transform levelButtonContainer;

    private void Start()
    {
        CreateLevelButton();
    }
    private void CreateLevelButton()
    {
        int levelCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 1; i < levelCount; i++)
        {
            LevelButton newLevelButton = Instantiate(levelButtonPrefab, levelButtonContainer);
            newLevelButton.SetUpLevelButton(i);
        }
    }
}
