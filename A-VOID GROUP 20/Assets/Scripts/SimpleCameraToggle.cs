using UnityEngine;
using UnityEngine.Diagnostics;

public class SimpleCameraToggle : MonoBehaviour
{
    public Camera menuCamera;
    public Camera MainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     public void ActivateGameplayView()
    {
        menuCamera.enabled = false;
        MainCamera.enabled = true;
    }

    // Update is called once per frame
   public void ActivateMenuView()
    {
        menuCamera.enabled=true;
        MainCamera.enabled=false;
    }
}
