using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI interactPrompt;
    [SerializeField] private TextMeshProUGUI saveMessage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void ShowPrompt(bool show)
    {
        if(interactPrompt  != null) 
        interactPrompt.gameObject.SetActive(show);
    }
    public void ShowSaveMessage(string message)
    {
        if (saveMessage != null)
        {
            saveMessage.text = message;
            StartCoroutine(FadeSaveMessage());
        }
    }
        private IEnumerator FadeSaveMessage()
    {
        saveMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        saveMessage.gameObject.SetActive(false);
    }
    }

  
