using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.5f; 
    public float magnitude = 0.3f;

    private Vector3 originalPos;
    private float elapsed = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        originalPos = transform.localPosition;
        elapsed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
        }
        else
        {
            transform.localPosition = originalPos;
            this.enabled = false; // stop shaking
        }
    }
          public void TriggerShake(float shakeDuration, float shakeMagnitude)
    {
        duration = shakeDuration;
        magnitude = shakeMagnitude;
        enabled = true;
        originalPos = transform.localPosition;
        elapsed = 0f;
    }
}
    

