using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIingame : MonoBehaviour
{
    public static UIingame instance;

    private Player playerHealth;
    [SerializeField] public RawImage HealthbarTotal;
    [SerializeField] public RawImage HealthbarCurrent;

    [SerializeField] private TextMeshProUGUI fruitText;

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
        SceneManager.LoadScene(0);
    }

    public void ReplayLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
