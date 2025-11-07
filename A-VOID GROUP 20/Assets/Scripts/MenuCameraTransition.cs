using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuCameraTransition : MonoBehaviour
{
    public Transform teleportTarget; 
    public float moveSpeed = 3f;
    public FadeManager fadeManager;  
    public AudioSource warpSound;  
    public string nextScene = "SampleScene";
    public Canvas menuCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isTransitioning = false;

    public void StartGame()
    {
        if (!isTransitioning)
            StartCoroutine(TransitionToGame());
    }

    IEnumerator TransitionToGame()
    {
        isTransitioning = true;

        // Optional: play sound
        if (warpSound != null)
            warpSound.Play();

        // Hide menu UI
        if (menuCanvas != null)
            menuCanvas.enabled = false;

        // Move camera toward target
        Transform cam = Camera.main.transform;
        Vector3 startPos = cam.position;
        Quaternion startRot = cam.rotation;

        Vector3 targetPos = teleportTarget.position;
        Quaternion targetRot = teleportTarget.rotation;

        float t = 0f;
        float duration = 1.5f;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            cam.position = Vector3.Lerp(startPos, targetPos, t);
            cam.rotation = Quaternion.Lerp(startRot, targetRot, t);
            yield return null;
        }

        // Fade to black
        if (fadeManager != null)
            fadeManager.FadeOut();

        yield return new WaitForSeconds(1f);

        // Load next scene
        SceneManager.LoadScene(nextScene);
    }
}
