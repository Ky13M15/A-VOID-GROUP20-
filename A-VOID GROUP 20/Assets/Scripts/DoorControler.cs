using UnityEngine;

public class DoorControler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;

    public AudioSource audioSource;
    public AudioClip openingDoor;
    public AudioClip closingDoor;

    private string oDoorState = "Door animations";
    private string cDoorState = "sliding";
    private bool wasOpened = false;
    private bool wasClosed = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();


    }

    public void PlayOpenDoorAudio()
    {
        if (audioSource != null && openingDoor != null)
        {
            audioSource.PlayOneShot(openingDoor);
        }
    }

    public void PlayCloseDoorAudio()
    {
        if (audioSource != null && closingDoor != null)
        {
            audioSource.PlayOneShot(closingDoor);
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Close");
        }
    }
   
}
