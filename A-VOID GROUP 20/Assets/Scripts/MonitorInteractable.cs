using UnityEngine;
using UnityEngine.Playables;

public class MonitorInteractable : MonoBehaviour,IInteractable
{
    public PlayableDirector introCutscene;
    public PlayableDirector missionCutscene;
    public GameObject objectivesUI;

    private bool hasPlayedIntro = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Interact()
    {
        if (!hasPlayedIntro)
        {
            introCutscene.Play();
            introCutscene.stopped += OnIntroCutsceneEnd;
            hasPlayedIntro=true;
        }
        else
        {
            missionCutscene.Play();
            ObjectiveManager.Instance.ShowObjective("Find the Jetpack and get off the planet!");
        }
    }

    // Update is called once per frame
    private void OnIntroCutsceneEnd(PlayableDirector director)
    {
        missionCutscene.Play();
        ObjectiveManager.Instance.ShowObjective("Find the Jetpack and get off the planet!");
    }
}
