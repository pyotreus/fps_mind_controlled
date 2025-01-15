using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public float speed = 15f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 4f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask interactionLayer;

    Vector3 velocity;

    private bool isGrounded;
    private readonly float interactionRange = 10f;
    private CharacterController characterController;

    //testing crystal vision
    public bool crystalActivated;
    protected Crystal crystal;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        crystal = GetComponent<Crystal>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            InventoryManager.Instance.ToggleInventory();
        }
        if (crystal.IsActive() || DialogueManager.Instance.IsDialogueActive())
        {
            return;
        }  

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(speed * Time.deltaTime * move);
        Jump();
        Interact();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && !crystalActivated)
        {
            CheckForInteraction();
        }
    }

    private void CheckForInteraction()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactionLayer))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            interactable?.Interact();
        } else
        {
            Debug.Log("nothing to interact");
        }
    }
}
