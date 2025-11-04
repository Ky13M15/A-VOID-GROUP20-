using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    public TextMeshProUGUI objectiveText;
    public CanvasGroup objectiveGroup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
      Instance = this;  
    }

    // Update is called once per frame
    public void ShowObjective(string text)
    {
      objectiveText.text = text;
      StopAllCoroutines();
        StartCoroutine(FadeIn());
    }
    private System.Collections.IEnumerator FadeIn()
    {
        objectiveGroup.alpha = 0; 
        while (objectiveGroup.alpha < 1)
        {
            objectiveGroup.alpha += Time.deltaTime * 2;
            yield return null;
        }
    }
}
