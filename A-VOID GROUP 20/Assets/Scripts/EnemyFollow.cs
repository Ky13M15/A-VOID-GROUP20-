using UnityEngine;
using UnityEngine.InputSystem.XR;
using System.Collections;
using static DaynNite;


public class EnemyFollow : MonoBehaviour
{
    public float speed = 3f; // Movement speed of the enemy
    private Transform player;
    public float playerDistance;
    public float minDistance, maxDistance;
    public CharacterController controller;

    public DaynNite daynNiteScript;

    public bool isDead = false;
    public float deathLag = 5f;

    public Animator animator;

    private Vector3 spawnPosition;

    public AudioSource audioSource;
    public string movementState = "Walk cycle";
    private bool wasMoving = false;


    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        spawnPosition = transform.position;
        
    }
    void Update()
    {

        playerDistance = (this.transform.position - player.position).magnitude;
        
        PlayerFollow();
        bool isMoving = animator.GetCurrentAnimatorStateInfo(0).IsName(movementState);

        if (isMoving && !wasMoving)
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (!isMoving && wasMoving)
        {
            
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        wasMoving = isMoving;
    }


    private void OnDrawGizmosSelected()
    {
        Vector3 center = transform.position;

        Gizmos.color = Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(center, maxDistance);
        //Max and Min
        Gizmos.color = Gizmos.color = Color.yellow;


        Gizmos.DrawWireSphere(center, minDistance);
    }

    public void PlayerFollow()
    {

        if ((player != null) && (daynNiteScript.timeOfDay == TimeOfDay.Nite))
        {
            // Calculate direction towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            direction = direction.normalized;
            // Move enemy towards the player
            //transform.position += direction * speed * Time.deltaTime;
            controller.Move(direction * speed * Time.deltaTime);

            Debug.Log("Following");

            animator.SetBool("isMoving", true);
            if (playerDistance <= minDistance)
            {
                animator.SetBool("canAttack", true);
                
            }
            if (playerDistance >= minDistance)
            {
                animator.SetBool("canAttack", false);
                Debug.Log("Walk");

            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDead) //change to bullet tag
        {
            KillMonster();
        }
    }

    void KillMonster()
    {
        isDead = true;
        if (animator != null)
        {
            animator.SetBool("isShot", true);
        }
        StartCoroutine(RespawnAfterLag());

    }

    private IEnumerator RespawnAfterLag()
    {
        yield return new WaitForSeconds(deathLag);

        transform.position = spawnPosition;
        isDead = false;

        transform.rotation = Quaternion.identity;
    }

    

}
