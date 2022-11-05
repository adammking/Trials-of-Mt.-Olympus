using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    private Animator animator;
    private DialogueTrigger dialogueTrigger;
    private Vector2 defaultFaceDirection;

    void Awake()
    {
        animator = GetComponent<Animator>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    public void RotateCharacter(float x, float y)
    {
        animator.SetFloat("moveX", x);
        animator.SetFloat("moveY", y);
    }

    public void StartDialogue()
    {
        dialogueTrigger.TriggerDialogue();
    }

}
