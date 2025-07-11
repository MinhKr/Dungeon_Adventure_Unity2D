using UnityEngine;
using UnityEngine.SceneManagement;
public class UIMainMenu : MonoBehaviour
{
    public string sceneName;

    [SerializeField] private GameObject[] uiElements;
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SwitchUI(GameObject uiToEnable)
    {
        foreach (GameObject ui in uiElements)
        {
            ui.SetActive(false);
        }
        uiToEnable.SetActive(true);

        AudioManager.instance.PlaySFX(4);
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
