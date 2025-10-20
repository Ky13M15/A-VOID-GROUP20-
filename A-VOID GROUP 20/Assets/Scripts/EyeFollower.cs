using Unity.VisualScripting;
using UnityEngine;

public class EyeFollower : MonoBehaviour
{
    public RectTransform eyeSocket, eye;
    public Canvas mainCavas;
    public float socketRange;
    public float eyeSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(eyeSocket, Input.mousePosition, null, out localPoint);
        Vector2 clampedTargetPos = Vector2.ClampMagnitude(localPoint, socketRange);
        eye.anchoredPosition = Vector2.Lerp(eye.anchoredPosition, clampedTargetPos, Time.unscaledDeltaTime * eyeSpeed);
        Debug.Log("locatPoint" + localPoint);
        
    }
}
