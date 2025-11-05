using UnityEngine;

public class FadeInOnStart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        var fade = FindFirstObjectByType<FadeManager>();
        if (fade != null)
            fade.FadeInInstant();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
