using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string levelSceneName = "Level1";

    public void Play()
    {
        SceneManager.LoadScene(levelSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}