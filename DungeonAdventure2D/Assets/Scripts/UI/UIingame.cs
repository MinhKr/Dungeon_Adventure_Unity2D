using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIingame : MonoBehaviour
{
    public static UIingame instance;

    [Header("Player Health UI")]
    private Player playerHealth;
    [SerializeField] public RawImage HealthbarTotal;
    [SerializeField] public RawImage HealthbarCurrent;

    [SerializeField] private TextMeshProUGUI fruitText;

    [Header("Player Death Info UI")]
    [SerializeField] private TextMeshProUGUI deathFruitCollectedText;
    [SerializeField] private TextMeshProUGUI deathLevelText;
    [SerializeField] private GameObject deathUI;

    [Header("Complete UI")]
    [SerializeField] private TextMeshProUGUI completedFruitCollectedText;
    [SerializeField] private TextMeshProUGUI completedLevelText;
    [SerializeField] private StarSystem starSystemComplete;
    [SerializeField] private GameObject completedUI;

    [Header("Star HUD")]
    [SerializeField] public StarSystem starSystemHUD;

    [Space]
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //InvokeRepeating(nameof(UpdatePlayerRef), 0, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    /*private void Start()
        {
            HealthbarTotal.uvRect = new Rect(playerHealth.currentHealth / 10f, 0, 1, 1);
        }*/

    /*public void UpdateHealthBar()
        {
            if (playerHealth != null)
                HealthbarCurrent.uvRect = new Rect(playerHealth.currentHealth / 10f, 0, 1, 1);
        }*/
    public void UpdateFruitText(int fruitCount)
    {
        if (fruitCount < 10)
            fruitText.text = "0" + fruitCount;
        else
            fruitText.text = fruitCount.ToString();
    }

    private void UpdatePlayerRef()
    {
        if (playerHealth == null)
            playerHealth = FindFirstObjectByType<Player>();
    }

    // Pause the game
    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            isPaused = false;
            pauseMenu.SetActive(false);
        }
    }

    public void GoToMainMenuUI()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ReplayLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevelIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ShowDeathUI(int fruitCollected)
    {
        Time.timeScale = 0f;
        deathFruitCollectedText.text = fruitCollected.ToString();
        deathLevelText.text = SceneManager.GetActiveScene().name;
        deathUI.SetActive(true);
    }

    public void ShowCompletedUI(int fruitCollected)
    {
        Time.timeScale = 0f;
        starSystemComplete.UpdateImageStar(GameManager.instance.starCollected);
        completedFruitCollectedText.text = fruitCollected.ToString();
        completedLevelText.text = SceneManager.GetActiveScene().name;
        completedUI.SetActive(true);
    }
}
