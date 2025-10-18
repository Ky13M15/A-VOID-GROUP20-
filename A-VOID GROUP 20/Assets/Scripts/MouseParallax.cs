using UnityEngine;

public class MouseParallax : MonoBehaviour
{
    [SerializeField]private float parallaxAmount = 0.05f;
    [SerializeField] private float smoothSpeed = 5f;

    private Vector3 initialPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     initialPosition = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = (Input.mousePosition.x / Screen.width) - 0.5f;
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;

        Vector3 targetPos = initialPosition + new Vector3(mouseX * parallaxAmount, mouseY * parallaxAmount, 0f);

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothSpeed);
    }
}
