using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1f; 
    }

    // Optional: quit to main menu
    public void QuitToMenu()
    {
        SceneManager.LoadScene("Main Menu Scene"); 
        Time.timeScale = 1f;
    }
}
