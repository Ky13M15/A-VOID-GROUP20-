using UnityEngine;

public class SpawnThings : MonoBehaviour
{
    public GameObject healthPack;
    

    // Update is called once per frame
    void Update()
    {

    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hitting : " + other.gameObject.name);
        Vector3 spawnPos = other.transform.position;

        Instantiate(healthPack, spawnPos, Quaternion.identity);

        


    }
}
