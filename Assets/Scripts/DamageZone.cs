using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{

    public float rate;
    public int damage;

    private float timeSinceLastUpdate;

    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyHeal();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Time.time - timeSinceLastUpdate >= rate)
        {
            ApplyHeal();
        }

    }

    private void ApplyHeal()
    {
        timeSinceLastUpdate = Time.time;
        playerStats.ApplyDamage(damage);
    }

}
