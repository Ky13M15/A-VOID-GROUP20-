using UnityEngine;

public class RayGunPickup : Interactable
{
    [Header("Pickup Settings")]
    public Transform equipPoint;           
    public GameObject raygunPrefab;        
    public bool destroyOnPickup = true;

    [Header("Unlock Condition")]
    public bool canPickup = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        promptMessage = "Press E to pick up Raygun";
    }


 
    protected override void Interact()
    {
        Debug.Log("Picked up Raygun!");

       
        FPController player = FindFirstObjectByType<FPController>();
        if (player == null)
        {
            Debug.LogWarning("No player found!");
            return;
        }

        
        player.hasWeapon = true;

        
        if (raygunPrefab != null && equipPoint != null)
        {
            GameObject newGun = Instantiate(raygunPrefab, equipPoint.position, equipPoint.rotation);
            newGun.transform.SetParent(equipPoint);
        }

        
        if (destroyOnPickup)
        {
            Destroy(gameObject);
        }
    }
}


