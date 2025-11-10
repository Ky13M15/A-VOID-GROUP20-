using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MonitorInteractable : Interactable
{


   
    public bool playTimelineInScene = true;
    public PlayableDirector cutsceneDirector; 
    public string cutsceneSceneName = "MissionCutScene";
    public bool disablePlayerDuringCutscene = true;

    [Header("UI Settings")]
    [SerializeField] private Canvas cutsceneCanvas;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        promptMessage = "Press E to access terminal";
        if (cutsceneCanvas != null)
            cutsceneCanvas.gameObject.SetActive(false);
    }
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.GetComponent<FPController>() != null)
        {
            hasTriggered = true;
            Debug.Log("Player entered monitor trigger — playing cutscene automatically!");
            Interact();
        }
    }
    protected override void Interact()
    {
        Debug.Log("Monitor accessed — starting mission cutscene!");

        if (cutsceneCanvas != null)
            cutsceneCanvas.gameObject.SetActive(true);

        // If Timeline is in the same scene
        if (playTimelineInScene && cutsceneDirector != null)
        {
            if (disablePlayerDuringCutscene)
            {
                FPController player = FindFirstObjectByType<FPController>();
                if (player != null)
                    player.enabled = false; // disables player movement
            }

            cutsceneDirector.stopped += OnCutsceneEnd;
            cutsceneDirector.Play();
        }
        else
        {
            SceneManager.LoadScene(cutsceneSceneName);
        }
    }

    private void OnCutsceneEnd(PlayableDirector director)
    {
        Debug.Log("Cutscene finished!");
        FPController player = FindFirstObjectByType<FPController>();
        if (player != null)
            player.enabled = true;

        if (cutsceneCanvas != null)
            cutsceneCanvas.gameObject.SetActive(false);




    }
}
