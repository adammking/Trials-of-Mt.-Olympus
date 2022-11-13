using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    private Animator animator;
    private DialogueTrigger dialogueTrigger;
    private Vector2 defaultFaceDirection;

    private bool _inCombat = false;
    public bool InCombat { get { return _inCombat; } protected set { _inCombat = value; } }

    public GameObject restFOV;
    public GameObject combatFOV;

    void Awake()
    {
        animator = GetComponent<Animator>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    public void RotateCharacter(float x, float y)
    {
        animator.SetFloat("moveX", x);
        animator.SetFloat("moveY", y);
        float rotation = CheckZRotation(x, y);
        if (_inCombat)
        {
            restFOV.transform.rotation = Quaternion.Euler(0, 0, rotation);
        } else if (!_inCombat)
        {
            combatFOV.transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
        
    }

    public void StartDialogue()
    {
        dialogueTrigger.TriggerDialogue();
    }

    private float CheckZRotation(float x, float y)
    {

        if(x == 1.0f)
        {
            return 90.0f;
        }
        if(x == -1.0f)
        {
            return 270.0f;
        }
        if(y == 1.0f)
        {
            return 180.0f;
        }
        return 0.0f;

    }

    public void EnterCombat()
    {
        _inCombat = true;
        restFOV.SetActive(false);
        combatFOV.SetActive(true);
    }

    public void LeaveCombat()
    {
        _inCombat = false;
        combatFOV.SetActive(false);
        restFOV.SetActive(true);
    }

}
