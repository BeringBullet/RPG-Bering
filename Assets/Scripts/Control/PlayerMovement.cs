using RPG.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float animationSpoothTime = 0.1f;
    [SerializeField] private float animationPlayTransition = 0.15f;

    private CharacterController controller;
    private PlayerConversant playerDialogue;
    //private PlayerInput playerInput;
    private Transform cameraTransform;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    //private InputAction moveAction;
    //private InputAction jumpAction;
    //public InputAction interactAction;
    //private InputAction RunAction;

    private Animator animator;
    int moveXAnimationHash;
    int moveZAnimationHash;
    int jumpAnimationHash;

    Vector2 currentAnimationBlend;
    Vector2 animationVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerDialogue = GetComponent<PlayerConversant>();
       // playerInput = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
        moveXAnimationHash = Animator.StringToHash("MoveX");
        moveZAnimationHash = Animator.StringToHash("MoveZ");
        jumpAnimationHash = Animator.StringToHash("Jump");
        cameraTransform = Camera.main.transform;

        //moveAction = playerInput.actions["Move"];
        //jumpAction = playerInput.actions["Jump"];
        //interactAction = playerInput.actions["Interact"];
        //RunAction = playerInput.actions["Run"];
    }

    void Update()
    {
        HandleDialogue();
        //HandleMovement();
    }

    private void HandleDialogue()
    {
        if (playerDialogue.IsActive())
        {
            Cursor.visible = true;
            //playerInput.actions.Disable();
        }
        else
        {
            Cursor.visible = false;
            //playerInput.actions.Enable();
        }
    }

    //private void HandleMovement()
    //{
    //    groundedPlayer = controller.isGrounded;
    //    if (groundedPlayer && playerVelocity.y < 0)
    //    {
    //        playerVelocity.y = 0f;
    //    }

    //    Vector2 input = moveAction.ReadValue<Vector2>();
    //    currentAnimationBlend = Vector2.SmoothDamp(currentAnimationBlend, input, ref animationVelocity, animationSpoothTime);
    //    Vector3 move = new Vector3(currentAnimationBlend.x, 0, currentAnimationBlend.y);
    //    move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
    //    move.y = 0f;
    //    controller.Move(move * Time.deltaTime * playerSpeed);
    //    animator.SetFloat(moveXAnimationHash, currentAnimationBlend.x);
    //    animator.SetFloat(moveZAnimationHash, currentAnimationBlend.y);
    //    // Changes the height position of the player..
    //    if (jumpAction.triggered && groundedPlayer)
    //    {
    //        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    //        animator.CrossFade(jumpAnimationHash, animationPlayTransition);

    //    }

    //    playerVelocity.y += gravityValue * Time.deltaTime;
    //    controller.Move(playerVelocity * Time.deltaTime);


    //    Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
    //    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    //}
}
