using UnityEngine;
using System.Collections;


public class DayNNiteAudioFade : MonoBehaviour
{
 
    public AudioClip dayClip;
    public AudioClip nightClip;
    public AudioSource daySource;
    public AudioSource nightSource;

    public float fadeDuration = 3f; // seconds to fade between sounds

    private DaynNite daynNite; // Reference to your time system
    private bool isDay;

    void Start()
    {

        daynNite = Object.FindFirstObjectByType<DaynNite>();

        // Start both audio sources
        daySource.clip = dayClip;
        nightSource.clip = nightClip;

        daySource.loop = true;
        nightSource.loop = true;

        daySource.Play();
        nightSource.Play();

        // Set initial volumes based on current time of day
        bool currentlyDay = daynNite != null && daynNite.timeOfDay == DaynNite.TimeOfDay.Day;
        isDay = currentlyDay;
        daySource.volume = isDay ? 1f : 0f;
        nightSource.volume = isDay ? 0f : 1f;
    }

    void Update()
    {
        if (daynNite == null) return;

        bool nowDay = daynNite.timeOfDay == DaynNite.TimeOfDay.Day;

        if (nowDay != isDay)
        {
            isDay = nowDay;
            StopAllCoroutines();
            StartCoroutine(FadeAudio(isDay));
        }
    }

    IEnumerator FadeAudio(bool toDay)
    {
        float t = 0f;

        float startDayVol = daySource.volume;
        float startNightVol = nightSource.volume;

        float targetDayVol = toDay ? 1f : 0f;
        float targetNightVol = toDay ? 0f : 1f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float normalized = t / fadeDuration;

            daySource.volume = Mathf.Lerp(startDayVol, targetDayVol, normalized);
            nightSource.volume = Mathf.Lerp(startNightVol, targetNightVol, normalized);

            yield return null;
        }

        daySource.volume = targetDayVol;
        nightSource.volume = targetNightVol;
    }
}


