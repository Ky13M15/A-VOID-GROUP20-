using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("References")]
    public GameObject menuCanvas;
    public GameObject playerController;
    public Camera menuCamera;
    public Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        playerController.SetActive(false);
        mainCamera.enabled = false;
        menuCamera.enabled = true;
    }

    // Update is called once per frame
    public void StartGame()
    {
        menuCanvas.SetActive(false);
        menuCamera.enabled = false;
        mainCamera.enabled = true;
        playerController.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
   