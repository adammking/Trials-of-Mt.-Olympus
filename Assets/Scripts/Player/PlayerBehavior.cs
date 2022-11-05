using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public float playerCollider = 0.2f;
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
            Vector3 currentPosition = transform.position;
            currentPosition.y -= 0.4f;
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

    void Move() {

        if (input.x != 0 && input.y != 0)
        {
            if (lastAxis == LastAxis.X)
            {
                movement.y = 0;
                movement.x = input.x;
            }
            if (lastAxis == LastAxis.Y)
            {
                movement.x = 0;
                movement.y = input.y;
            }
        } else
        {
            movement = input;
        }

        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);

        Vector3 targetPos = transform.position;
        targetPos.x += movement.x;
        targetPos.y += movement.y;

        if (IsWalkable(targetPos))
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
        }
    }

    private bool IsWalkable(Vector3 targetPos) 
    {
        /*var previousTargetPos = targetPos;

        if (input.x != 0 && input.y != 0)
        {

        }
        else if (input.y == 0)
        {
            targetPos.y -= 0.4f;

            if (input.x > 0)
            {
                targetPos.x -= 0.6f;
            }
            else if (input.x < 0)
            {
                targetPos.x += 0.6f;
            }
        }
        else if (input.x == 0)
        {
            if (input.y >= 0)
            {
                targetPos.y -= 1.2f;
            }
            else if (input.y < 0)
            {
                targetPos.y += 0.6f;
            }
        }*/

        Vector3 origin = transform.position;
        origin.y -= 0.5f;

        float distance = 0.35f;
        if (animator.GetFloat("moveY") < 0)
        {
            distance = 0.25f;
        }

        if (Physics2D.BoxCast(origin, new Vector2(0.55f, 0.55f), 0f, movement, distance, solidObjectsLayer | interactableLayer))
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

