using UnityEngine;
using UnityEngine.SceneManagement;

public class JetpackInteractable : Interactable
{
   protected override void Interact()
    {
        Debug.Log("Jetpack picked up! Loading outro cutscene...");
        SceneManager.LoadScene("OutroCutscene");
    }
}
