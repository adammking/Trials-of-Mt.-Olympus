using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    public float interactDistance = 1.15f;

    public GameObject dialoguePanel;
    private bool inDialogue = false;
    private NPCScript dialoguePartner;

    private bool isMoving;
    private Vector2 input;
    private Vector2 movement;
    public enum LastAxis { None, X, Y }
    public LastAxis lastAxis = LastAxis.None;

    private Animator animator;

    private void Awake() 
    {

        animator = GetComponent<Animator>();

    }

    private void Update()
    {

        if (input.x == 0 && Input.GetAxisRaw("Horizontal") != 0) lastAxis = LastAxis.X;
        if (input.y == 0 && Input.GetAxisRaw("Vertical") != 0) lastAxis = LastAxis.Y;

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input != Vector2.zero && !inDialogue)
        {
            isMoving = true;
            Move();
        } else
        {
            isMoving = false;
        }

        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }

    }

    void Interact()
    {

        if (!inDialogue)
        {

            // Lowers position of Raycast origin to be nearer the sprites feet
            Vector3 currentPosition = transform.position;
            currentPosition.y -= 0.4f;

            // Raycast to see if anything interactable is in front of the player
            RaycastHit2D objectHit = Physics2D.Raycast(currentPosition, movement, interactDistance, interactableLayer);
            if (objectHit)
            {

                dialoguePartner = objectHit.collider.GetComponent<NPCScript>();
                dialoguePartner.RotateCharacter(movement.x * -1.0f, movement.y * -1.0f);
                dialoguePartner.StartDialogue();
                dialoguePanel.SetActive(true);
                inDialogue = true;

            }
        } 
        else if (inDialogue)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
        
    }

    void Move() 
    {

        // Handles multiple movement keys being held down
        if (input.x != 0 && input.y != 0)
        {
            if (lastAxis == LastAxis.X) movement = new Vector2(input.x, 0);
            if (lastAxis == LastAxis.Y) movement = new Vector2(0, input.y);
        } 
        else
        {
            movement = input;
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
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Changes movement state when not able to continue walking
            isMoving = false;
        }

    }

    private bool PathIsWalkable() 
    {
        // Lowers position of Raycast origin to be nearer the sprites feet
        Vector3 origin = transform.position;
        origin.y -= 0.5f;

        // Checks if unmoveable objects are in player path
        if (Physics2D.BoxCast(origin, new Vector2(0.55f, 0.55f), 0f, movement, 0.35f, solidObjectsLayer | interactableLayer))
        {
            return false;
        }
        
        return true;
    }

    public void EndDialogue()
    {
        inDialogue = false;
        dialoguePanel.SetActive(false);
        dialoguePartner.RotateCharacter(0f, 0f);
    }

}

