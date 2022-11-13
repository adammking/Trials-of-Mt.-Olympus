using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostPatch : MonoBehaviour
{

    public float abilityLength;

    private float timeAbilityUsed;

    private void Start()
    {
        timeAbilityUsed = Time.time;
    }

    private void Update()
    {

        if (Time.time - timeAbilityUsed > abilityLength)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            collision.GetComponent<PlayerBehavior>().ChangeMovementSpeed(true);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            collision.GetComponent<PlayerBehavior>().ChangeMovementSpeed(false);

        }

    }



}
