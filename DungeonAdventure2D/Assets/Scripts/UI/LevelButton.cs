using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Sprite starOnSprite;
    [SerializeField] private Sprite starOffSprite;
    [Space]
    [Header("Properties")]
    [SerializeField] private Image[] stars;
    [SerializeField] private TextMeshProUGUI levelNumberText;
    private int levelIndex;
    private string sceneName;

    [SerializeField] private GameObject lockIcon;     
    [SerializeField] private Button levelButton;  

    public void SetUpLevelButton(int newLevelIndex)
    {
        levelIndex = newLevelIndex;
        levelNumberText.text = levelIndex.ToString();
        sceneName = "Level " + levelIndex.ToString();    

        UpdateLockState();
        UpdateStarsUI();
    }

    private void UpdateStarsUI()
    {
        string key = "Level" + levelIndex + "_Star";
        int savedStars = PlayerPrefs.GetInt(key, 0);

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].sprite = i < savedStars ? starOnSprite : starOffSprite;
        }
    }

    private void UpdateLockState()
    {
        bool isUnlocked = levelIndex == 1 || PlayerPrefs.GetInt("Level" + levelIndex + "_Unlocked", 0) == 1;

        if (lockIcon != null)
            lockIcon.SetActive(!isUnlocked);

        if (levelButton != null)
            levelButton.interactable = isUnlocked;

        foreach (var star in stars)
        {
            star.gameObject.SetActive(isUnlocked);
        }
    }

    public void LoadLevel()
    {
        AudioManager.instance.PlaySFX(4);
        SceneManager.LoadScene(sceneName);
    }
}
