using UnityEngine;

public class SaveStation : MonoBehaviour
{
    [HideInInspector] public bool playerInRange = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Playter"))
        {
            playerInRange = true;
            UIManager.Instance.ShowPrompt(false);
        }
    }
    public void Interact()
    {
        if (playerInRange)
        {
            SaveSystem.SavePlayer();
            UIManager.Instance.ShowSaveMessage("Game Saved!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
