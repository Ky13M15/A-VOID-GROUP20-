using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        FadeManager fade = FindFirstObjectByType<FadeManager>();
        if (fade != null)
            fade.FadeOut();

        Invoke("LoadNextScene", 1f);
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

}
