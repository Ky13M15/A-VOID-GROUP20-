using UnityEngine;

[RequireComponent(typeof(Transform))]
public class ParallexLayer : MonoBehaviour
{
    [Range(0f, 1f)]
    public float parallaxFactor = 0.5f;

    Vector3 startPos;
    Transform cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.transform;
        startPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 camDelta = cam.position - Camera.main.transform.position;
        Vector3 mouseOffset = GetMouseParallax() * (1f - parallaxFactor);
        transform.position = startPos + new Vector3(mouseOffset.x, mouseOffset.y, 0) + camDelta * (1 - parallaxFactor);
    }
    Vector2 GetMouseParallax()
    {
        Vector2 mp = Vector2.zero;
        if (Camera.main == null) return mp;
        Vector2 screenCentre = new Vector2(Screen.width, Screen.height) * 0.5f;
        Vector2 diff = (Vector2)Input.mousePosition - screenCentre;
        float normX = diff.x / screenCentre.x;
        float normY = diff.y / screenCentre.y;

        return new Vector2(normX, normY) * 0.15f;

    }
}
