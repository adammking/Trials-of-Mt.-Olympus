using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class AoeSpell : MonoBehaviour
{

    public int damage;
    public float damageTick;
    public float abilityLength;

    private float timeSinceDamageTick;
    private float timeAbilityUsed;

    private void Start()
    {
        timeAbilityUsed = Time.time;
    }

    private void Update()
    {
        
        if(Time.time - timeAbilityUsed > abilityLength)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            timeSinceDamageTick = Time.time;
            PlayerStats.takeDamage(damage);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Time.time - timeSinceDamageTick >= damageTick && collision.CompareTag("Player"))
        {
            timeSinceDamageTick = Time.time;
            PlayerStats.takeDamage(damage);
        }

    }

}
