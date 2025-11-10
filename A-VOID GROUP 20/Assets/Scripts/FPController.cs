using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FPController : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    private InputActionAsset interactAction;
    public Camera playerCamera;
    public float normalFOV = 60f;
    public float aimFOV = 40f;
    public float aimSpeed = 10f;
    private Controls1 controls;



    public LazerMuzzleFlash muzzleFlash;

  
   
    [SerializeField] private float interactRange = 3f;
    [SerializeField] LayerMask interactLayer;
    
  


    [SerializeField] private GameObject DeathScreen;





    private bool isAiming = false;

    //aiming event callback
    public event Action<bool> onAimChanged;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 90f;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;


    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform gunPoint;

    [Header("Crouch Settings")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float crouchSpeed = 2.5f;
    private float originalMoveSpeed;

    [Header("Pickup Settings")]
    public float pickupRange = 3f;
    public Transform holdPoint;
    private PickupObject heldObject;
    public TMP_Text pickupText;


    [Header("Throwing settings")]
    public float throwForce = 10f;
    public float throwUpwardBoost = 1f;

    [Header("Sprint settings")]
    [SerializeField] private float sprintMultiplier;
    private bool isSprinting=false;

    public PauseMenu pauseMenuScript;

    private Animator animator;




    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        originalMoveSpeed = moveSpeed;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        
        controls = new Controls1();
        controls.Player.Enable();

        controls.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        controls.Player.Jump.performed += OnJump;
        controls.Player.Interact.performed += OnInteract;
        controls.Player.Pickup.performed += OnPickup;
        controls.Player.Throwing.performed += OnThrow;
        controls.Player.Aiming.started += OnAim;
        controls.Player.Aiming.canceled += OnAim;
        controls.Player.Sprint.started += OnSprint;
        controls.Player.Sprint.canceled += OnSprint;
        controls.Player.Crouch.started += OnCrouch;
        controls.Player.Crouch.canceled += OnCrouch;
        controls.Player.Shoot.performed += OnShoot;



        playerCamera = Camera.main;

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }



    }
    private void Start()
    {
        DeathScreen.SetActive(false);

        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        HandleMovement();
        HandleLook();

        if (heldObject != null)
        {
            heldObject.MoveToHoldPoint(holdPoint.position);
        }
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, interactLayer))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                pickupText.text = interactable.promptMessage;
            }
            else
            {
                pickupText.text = "";
            }
        }
        else
        {
            pickupText.text = "";
        }
        float targetFOV = isAiming ? aimFOV : normalFOV;
        float newFOV = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * aimSpeed);

    }



   


    







    public void OnMove(InputAction.CallbackContext context) => moveInput = context.ReadValue<Vector2>();


    public void OnPickup(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (heldObject == null)
        {
            Ray ray = new(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactLayer))
            {
                if (hit.collider.GetComponent<IInteractable>() != null)
                {
                    pickupText.text = "Press E to interact";
                }
                else
                {
                    pickupText.text = "";
                }
            }

            {
                if (hit.collider.GetComponent<PickupObject>() != null)
                {
                    hit.collider.GetComponent<PickupObject>().Pickup(holdPoint);
                    heldObject = hit.collider.GetComponent<PickupObject>();

                }
            }
        }
        else
        {
            heldObject.Drop();
            heldObject = null;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            //animator.SetBool("isJumping", true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        /*else
        {
            animator.SetBool("isJumping", false);
        }*/

    }
    public void OnThrow(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (heldObject == null) return;

        Vector3 dir = cameraTransform.forward;
        Vector3 implulse = dir * throwForce + Vector3.up * throwUpwardBoost;

        heldObject.Throw(implulse);
        heldObject = null;

    }
    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAiming = true;

            onAimChanged?.Invoke(true);
        }
        else if (context.canceled)
        {
            isAiming = false;
            onAimChanged?.Invoke(false);
        }


    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed) return;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactLayer))
        {
            if (hit.collider.CompareTag("Jetpack"))
            {
                Debug.Log("Jetpack found! Loading outro cutscene...");
                UnityEngine.SceneManagement.SceneManager.LoadScene("OutroCutScene");
                return;
            }
        
        
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.BaseInteraction();
            }
            else
            {
                Debug.Log("interacted with:" + hit.collider.name);
            }
        }

        else
        {
            Debug.Log("Nothing to interact with");
        }
    }


public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }

    public void OnLook(InputAction.CallbackContext context) => lookInput = context.ReadValue<Vector2>();
    private void Shoot()
    {
        if (!pauseMenuScript.GameIsPaused)
        {
            Debug.Log("Bool: " + pauseMenuScript.GameIsPaused);
            if (bulletPrefab != null && gunPoint != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddForce(gunPoint.forward * 1000f,ForceMode.Impulse);
                    Destroy(bullet, 2.5f);
                }
                if(muzzleFlash != null)
                {
                    muzzleFlash.Fire(gunPoint.position, gunPoint.forward);
                }
            }
           

        }
        else
        {
            Cursor.visible = true;
            Debug.Log("Bool: " + pauseMenuScript.GameIsPaused);
            Cursor.lockState = CursorLockMode.None;

        }

        Debug.Log("shoot");
    }
        public void OnSprint(InputAction.CallbackContext context) {
        if (context.performed)
        {
            isSprinting = true;
            
        }
        else if (context.canceled)
        {
            isSprinting = false;
            
        }

    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.height = crouchHeight;
            moveSpeed = originalMoveSpeed;
        }
        else if (context.canceled)
        {
            controller.height = standHeight;
            moveSpeed = originalMoveSpeed;
        }
    }

    private void HandleMovement()
    {
        float currentSpeed = moveSpeed * (isSprinting ? sprintMultiplier: 1f);
        Vector3 move = transform.right * moveInput.x + transform.forward *
        moveInput.y;
        controller.Move(moveSpeed * Time.deltaTime * move);

        bool isMoving = move.magnitude > 0.1f;
        //animator.SetBool("isMoving", isMoving);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
    private void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit,
        verticalLookLimit);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
  
      
        

    }

