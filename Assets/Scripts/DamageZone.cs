using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{

    public float rate;
    public int damage;

    private float timeSinceLastUpdate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ApplyDamage();
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Time.time - timeSinceLastUpdate >= rate && collision.CompareTag("Player"))
        {
            ApplyDamage();
        }

    }

    private void ApplyDamage()
    {
        timeSinceLastUpdate = Time.time;
        PlayerStats.takeDamage(damage);
    }

}
