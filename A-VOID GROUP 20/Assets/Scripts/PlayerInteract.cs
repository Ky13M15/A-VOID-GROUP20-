using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Controls1 inputActions;
    private SaveStation currentStation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inputActions = new Controls1();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += ctx => TryInteract();
    }
    private void OnTriggerEnter(Collider other)
    {
        SaveStation station = other.GetComponent<SaveStation>();
        if (station != null)
        {
            currentStation = station;
        }
        void OnTriggerExit(Collider other)
       
        {
            SaveStation station = other.GetComponent<SaveStation>();
            if (station != null && station == currentStation) 
            { 
                currentStation = null; 
            }
        }
    }

    // Update is called once per frame
    void TryInteract()
    {
        if (currentStation != null)
        {
            currentStation.Interact();
        }

    }
}
