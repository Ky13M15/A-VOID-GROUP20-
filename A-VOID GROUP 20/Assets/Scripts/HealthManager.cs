using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxBars = 8;
    public int currentBars;

   
    [Header("UI References")]
    public Image[] healthBars; 
    public GameObject DeathScreen;

    [Header("Respawn Settings")]
    public Transform spawnPoint;
    public bool reloadSceneOnRespawn = false;

    private FPController fpController;
    private bool isDead = false;



    void Start()
    {
        fpController = GetComponent<FPController>();
        currentBars = maxBars;
        UpdateHealthUI();

        if (DeathScreen != null) DeathScreen.SetActive(false);
    }

    public void TakeDamage(int damageBars =1)
    {
        if (isDead) return;

        currentBars -= damageBars;
        currentBars = Mathf.Clamp(currentBars, 0, maxBars);
        UpdateHealthUI();

        if (currentBars <= 0)
        {
            Death();
        }
    }

    
    public void Heal(int bars = 2)
    {
        if (isDead) return;

        currentBars += bars;
        currentBars = Mathf.Clamp(currentBars, 0, maxBars);
        UpdateHealthUI();
    }
    private void UpdateHealthUI()
    {
        for (int i = 0; i < healthBars.Length; i++)
        {
            if (healthBars[i] != null)
            {
                healthBars[i].enabled = i < currentBars;
            }
        }
    }

    // Update is called once per frame
    private void Death()
    {
        if (isDead) return;
        isDead = true;

        fpController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (DeathScreen != null) DeathScreen.SetActive(true);
        Debug.Log("GAME OVER!");

    }
    public void ResetHealth()
    {
        currentBars = maxBars;
        UpdateHealthUI();
        isDead = false;

        if (DeathScreen != null) DeathScreen.SetActive(false);

        if (reloadSceneOnRespawn)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }

        fpController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
