using UnityEngine;



public class Door : MonoBehaviour, IInteractable
{
    private bool isOpen = false;

    public void Interact()
    {
        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        transform.Rotate(0, 90, 0);
        isOpen = true;
        Debug.Log("Door opened!");
    }

    void CloseDoor()
    {
        transform.Rotate(0, -90, 0);
        isOpen = false;
        Debug.Log("Door closed!");
    }
}

