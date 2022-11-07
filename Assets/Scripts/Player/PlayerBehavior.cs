using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // Start is called before the first frame update\
    public float regularMoveSpeed;
    public float slowedMoveSpeed;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    public float interactDistance = 1.15f;
    public Animator animator;
    public PlayerInput playerInput;
    public GameObject dialoguePanel;
    public float feetHitboxOffset;

    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } protected set { moveSpeed = value; } }

    private bool _inDialogue = false;
    public bool InDialogue { get { return _inDialogue; } protected set { _inDialogue = value; } }
    private NPCBehavior dialoguePartner;

    private bool _isMoving;
    public bool IsMoving { get { return _isMoving; } set { _isMoving = value; } }

    private bool _inMenu;
    public bool InMenu { get { return _inMenu; } set { _inMenu = value; } }

    private Vector2 movement;
    public Vector2 Movement { get { return movement; } protected set { movement = value; } }

    private void Awake()
    {
        moveSpeed = regularMoveSpeed;
    }

    public void TakeDamage()
    {
        PlayerStats.takeDamage(20);
    }

    public void Heal()
    {
        PlayerStats.takeHeal(20);
    }
    public void BringBackToLife()
    {
        PlayerStats.bringToLife();
        
    }

    public void Interact()
    {

        if (!_inDialogue)
        {

            // Lowers position of Raycast origin to be nearer the sprites feet
            Vector3 currentPosition = transform.position;
            currentPosition.y -= 0.4f;

            // Raycast to see if anything interactable is in front of the player
            RaycastHit2D objectHit = Physics2D.Raycast(currentPosition, movement, interactDistance, interactableLayer);
            if (objectHit)
            {

                dialoguePartner = objectHit.collider.GetComponent<NPCBehavior>();
                dialoguePartner.RotateCharacter(movement.x * -1.0f, movement.y * -1.0f);
                dialoguePartner.StartDialogue();
                PlayerHUDParent.toggleDialogueWindow();
                _inDialogue = true;

            }
        } 
        else if (_inDialogue)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
        
    }

    public void Move() 
    {

        // Handles multiple movement keys being held down
        if (playerInput.input.x != 0 && playerInput.input.y != 0)
        {
            if (playerInput.lastAxis == PlayerInput.LastAxis.X) movement = new Vector2(playerInput.input.x, 0);
            if (playerInput.lastAxis == PlayerInput.LastAxis.Y) movement = new Vector2(0, playerInput.input.y);
        } 
        else
        {
            movement = playerInput.input;
        }

        // Sets animation state based on current movement direction
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);

        Vector3 targetPos = transform.position;
        targetPos.x += movement.x;
        targetPos.y += movement.y;


        if (PathIsWalkable())
        {
            // Moves character if the path ahead is walkable
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Changes movement state when not able to continue walking
            _isMoving = false;
        }

    }

    private bool PathIsWalkable() 
    {
        // Lowers position of Raycast origin to be nearer the sprites feet
        Vector3 origin = transform.position;
        origin.y -= feetHitboxOffset;

        // Checks if unmoveable objects are in player path
        if (Physics2D.BoxCast(origin, new Vector2(0.55f, 0.55f), 0f, movement, 0.35f, solidObjectsLayer | interactableLayer))
        {
            return false;
        }
        
        return true;
    }

    public void EndDialogue()
    {
        _inDialogue = false;
        PlayerHUDParent.toggleDialogueWindow();
        dialoguePartner.RotateCharacter(0f, 0f);
        dialoguePartner = null;
    }

    public void ChangeMovementSpeed(bool isSlowed)
    {

        if (isSlowed)
        {
            moveSpeed = slowedMoveSpeed;
            animator.speed = 0.5f;
        }
        else
        {
            moveSpeed = regularMoveSpeed;
            animator.speed = 1.0f;
        }

    }



}

