using UnityEngine;

public class DoorControler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;

     public AudioSource DoorAudio;
    bool DoorOpening;

    public AudioClip doorOpening;
    public AudioClip doorClosing;
    private string openingState = "Door animations";
    private string closingState = "sliding";
    private bool wasOpened = false;
    private bool wasClosed = false;

    void Start()
    {
        DoorOpening = false;
        animator = GetComponent<Animator>();
        DoorAudio = GetComponent<AudioSource>();
    }


    void Update()
    {
        bool isOpened = animator.GetCurrentAnimatorStateInfo(0).IsName(openingState);
        if (isOpened && !wasOpened)
        {
            if (DoorAudio != null && doorOpening != null)
            {
                DoorAudio.PlayOneShot(doorOpening);
            }
        }
        wasOpened = isOpened;
        bool isClosed = animator.GetCurrentAnimatorStateInfo(0).IsName(closingState);
        if (isClosed && !wasClosed)
        {
            if (DoorAudio != null && doorClosing != null)
            {
                DoorAudio.PlayOneShot(doorClosing);
            }
        }
        wasClosed = isClosed;
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
