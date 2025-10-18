using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PhysicalMenuButton : MonoBehaviour
{
    public UnityEvent onPress;
    public AudioClip pressSound;
    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     audioSource = gameObject.AddComponent<AudioSource>();   
    }
     void OnMouseDown()
    {
        onPress?.Invoke();
        if(pressSound)audioSource.PlayOneShot(pressSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
