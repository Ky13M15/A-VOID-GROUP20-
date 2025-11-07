using UnityEngine;

public class LazerMuzzleFlash : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Light flashLight;
    public float flashDuration = 0.05f;
    public float maxDistance = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       if (lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();
       if (flashLight == null) flashLight = GetComponent<Light>();
       flashLight.enabled = false;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    public void Fire (Vector3 origin, Vector3 direction )
    {
        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance))
        {
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition (1,hit.point);
        }
        else
        {
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition (1,origin+ direction * maxDistance);
        }
        StartCoroutine(FlashRoutine());
    }
    private System.Collections.IEnumerator FlashRoutine()
    {
        lineRenderer.enabled = true;
        flashLight.enabled = true;

        yield return new WaitForSeconds(flashDuration);

        lineRenderer.enabled = false;
        flashLight.enabled =false;
    }
}
