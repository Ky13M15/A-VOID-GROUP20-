using UnityEngine;
using UnityEngine.SceneManagement;

public class JetpackPickup : MonoBehaviour
{
    public string outroSceneName = "OutroCutScene";
    private bool triggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            SceneManager.LoadScene(outroSceneName);
        }
    }

    
  
    }

