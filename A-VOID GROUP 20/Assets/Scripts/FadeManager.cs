using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public Animator fadeAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void FadeOut()
    {
        if (fadeAnim != null)
            fadeAnim.SetTrigger("FadeOut");
    }
    public void FadeInInstant()
    {
        if (fadeAnim != null)
            fadeAnim.Play("FadeIdle", 0, 0f); 
    }
}
