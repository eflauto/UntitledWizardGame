using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        MainManager.Instance.UnpauseGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
