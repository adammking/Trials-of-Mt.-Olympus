using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private Vector2 _input;
    public Vector2 input
    { 
        get { return _input; } 
        protected set { _input = value; }
    }

    public enum LastAxis { None, X, Y }
    public LastAxis lastAxis = LastAxis.None;

    public PlayerBehavior playerBehavior;
    public PlayerStats playerStats;
    public PlayerAttacks playerAttacks;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            playerBehavior.BringBackToLife();
        }

        if (playerStats.IsAlive)
        {

            if (_input.x == 0 && Input.GetAxisRaw("Horizontal") != 0) lastAxis = LastAxis.X;
            if (_input.y == 0 && Input.GetAxisRaw("Vertical") != 0) lastAxis = LastAxis.Y;

            _input.x = Input.GetAxisRaw("Horizontal");
            _input.y = Input.GetAxisRaw("Vertical");


            if (_input != Vector2.zero && !playerBehavior.InDialogue)
            {
                playerBehavior.IsMoving = true;
                playerBehavior.Move();
            }
            else
            {
                playerBehavior.IsMoving = false;
            }

            playerBehavior.animator.SetBool("isMoving", playerBehavior.IsMoving);


            if (Input.GetKeyDown(KeyCode.Z))
            {
                playerBehavior.Interact();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                playerBehavior.TakeDamage();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                playerBehavior.Heal();
            }

            if (Input.GetMouseButtonDown(0))
            {
                playerAttacks.SpawnDamageZone();
            }

        }
    }
}
