using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private Controls1 inputActions;
    private IInteractable currentInteractable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inputActions = new Controls1();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += ctx => TryInteract();
    }
    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            currentInteractable = interactable;
        }
    }
       private void OnTriggerExit(Collider other)
       
        {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable!= null && interactable == currentInteractable) 
            { 
                currentInteractable = null; 
            }
        }
    private void TryInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
}

   
