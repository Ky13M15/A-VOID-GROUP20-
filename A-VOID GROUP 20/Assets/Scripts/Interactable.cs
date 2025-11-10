using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promptMessage;
    public void BaseInteraction()
    {
        Interact();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  protected virtual void Interact()
    {

    }
}
