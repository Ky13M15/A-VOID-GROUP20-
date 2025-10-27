using UnityEngine;

public class MeteorImpact : MonoBehaviour
{
    public CameraShake cameraShake;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter(Collision collision)
    {
        cameraShake.TriggerShake(0.5f, 0.5f);
    }
}
