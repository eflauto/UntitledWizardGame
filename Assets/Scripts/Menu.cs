using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        MainManager.Instance.UnpauseGame();
        MainManager.Instance.health = MainManager.Instance.maxHealth;
        MainManager.Instance.mana = MainManager.Instance.maxMana;
        MainManager.Instance.unlockedSpellsCount = 0;
        MainManager.Instance.SetWaypoint("TowerSpawn");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Resume()
    {
        MainManager.Instance.UnpauseGame();
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        Resume();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
}
