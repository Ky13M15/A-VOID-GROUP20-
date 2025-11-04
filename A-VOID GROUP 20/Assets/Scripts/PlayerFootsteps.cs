using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource footstepsAudio;
    public AudioClip footstepClip;
    public float footestepDelay = 0.5f;

    public float stepTimer;
    private CharacterController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        footstepsAudio.clip = footstepClip;
        stepTimer = footestepDelay;
    }

    // Update is called once per frame
    void Update()
    {
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        if (isMoving && controller.isGrounded)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer < 0)
            {
                footstepsAudio.PlayOneShot(footstepClip);
                stepTimer = footestepDelay;
            }
        }
        else
        {
            stepTimer = 0.1f;
        }
    }
}
