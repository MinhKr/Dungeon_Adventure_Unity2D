using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_MainMenu : MonoBehaviour
{
    public string sceneName;
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
