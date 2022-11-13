using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour
{

    public float rate;
    public int healValue;

    private float timeSinceLastUpdate;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyHeal();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time - timeSinceLastUpdate >= rate)
            {
                ApplyHeal();
            }
        }

    }

    private void ApplyHeal()
    {
        timeSinceLastUpdate = Time.time;
        PlayerStats.takeHeal(healValue);
    }

}
