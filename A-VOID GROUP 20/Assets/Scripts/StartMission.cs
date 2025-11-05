using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartMission : MonoBehaviour
{
    public FadeManager fadeManager;
    public AudioSource warpSound;
    public string nextScene = "SampleScene";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void OnStartPressed ()
    {
        StartCoroutine(StartSequence());
    }
    IEnumerator StartSequence()
    {
        if (warpSound != null)
            warpSound.Play();


        fadeManager.FadeOut();

       
     yield return new WaitForSeconds(1.5f);

       
        SceneManager.LoadScene(nextScene);
    }
}

   
