using UnityEngine;

public class FireLightFlicker : MonoBehaviour
{
    public Light fireLight;
    public float minIntensity = 3f;
    public float maxIntensity = 6f;
    public float flickerSpeed = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if ( fireLight == null )
            { fireLight = GetComponent<Light>(); }
    }

    // Update is called once per frame
    void Update()
    {
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0.0f);
        fireLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);


















    }
}
