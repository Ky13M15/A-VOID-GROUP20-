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
    private bool isAttacking = false;


    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        spawnPosition = transform.position;
    }
    void Update()
    {

        playerDistance = (this.transform.position - player.position).magnitude;
        if (playerDistance <= minDistance)
        {
            PlayerFollow();
        }
        else if (playerDistance >= maxDistance)
        {
            Debug.Log("Player is too far");
        }

        if (playerDistance <= minDistance && !isAttacking)
        {
            StartAttack();
        }
        else if (playerDistance > maxDistance && isAttacking)
        {
            StopAttack();
        }
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

    void StartAttack()
    {
        isAttacking = true;
        animator.SetBool("IsNear", true);
    }
    void StopAttack()
    {
        isAttacking = false;
        animator.SetBool("IsNear", false);
    }

}
