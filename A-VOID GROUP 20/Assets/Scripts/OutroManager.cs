using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class OutroManager : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject gameOverUI;
    public Image fadeImage;

    [Header("Scene Transition")]
    public string mainMenuScene = "Main Menu Scene";
    public float fadeDuration = 2f;
    public float delayBeforeGameOver = 1.5f;
    public float delayBeforeMenu = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (timeline != null)
        {
            timeline.stopped += OnCutsceneEnd;
            timeline.Play();
        }
        if(fadeImage != null)
            fadeImage.gameObject.SetActive(true);
    }

    // Update is called once per frame
    
  private void OnCutsceneEnd(PlayableDirector director)
    {
        StartCoroutine(EndSequence());
    }
    private IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(delayBeforeGameOver);
        yield return StartCoroutine(FadeToBlack());

        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }
    private IEnumerator FadeToBlack()
    {
        if (fadeImage == null) yield break;

        Color color = fadeImage.color;
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }
}

