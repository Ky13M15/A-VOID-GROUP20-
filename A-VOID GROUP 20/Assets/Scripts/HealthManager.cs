using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

   
    [Header("UI References")]
    public Image healthBar; 
    public GameObject DeathScreen;

    [Header("Respawn Settings")]
    public Transform spawnPoint;
    public bool reloadSceneOnRespawn = false;

    public Animator bloodSplashAnimator;



    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        if (DeathScreen != null) DeathScreen.SetActive(false);

    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0,maxHealth);

        if (bloodSplashAnimator  != null)
        {
            bloodSplashAnimator.SetTrigger("ShowSplash");
        }

        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Death();
        }
    }
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth= Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }
    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

        // Update is called once per frame
        private void Death()
    {
        GetComponent<FPController>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (DeathScreen != null) DeathScreen.SetActive(true);
        Debug.Log("GAME OVER!");

    }
    public void ResetHealth()
    {
        currentHealth= maxHealth;
        UpdateHealthUI();
        if(DeathScreen != null) DeathScreen.SetActive(false);

        if (reloadSceneOnRespawn)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (spawnPoint != null)
        {
            transform.position = spawnPoint.position; 
            transform.rotation = spawnPoint.rotation; 
        }
        GetComponent<FPController>().enabled = true ;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
  

}
