using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("References")]
   
    public GameObject MainMenuPanel;
     public GameObject controlsPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    public void StartGame()
    {
       
    }
    
   

    // Called when the Controls button is pressed
    public void ShowControls()
    {
        MainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    // Called when the Back button is pressed
    public void ShowMainMenu()
    {
         MainMenuPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }
}
   