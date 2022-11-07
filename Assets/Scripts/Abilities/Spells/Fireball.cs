using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour, IAbility
{
    public float speed;
    public float maxTravelDistance;
    public int damage;

    private bool firedByNpc;
    private float distanceTraveled = 0.0f;
    private Vector3 fireDirection;
    private Vector3 lastPosition;

    private void Update()
    {
        
        if(fireDirection != null)
        {
            lastPosition = transform.position;
            transform.position += speed * Time.deltaTime * fireDirection;
            distanceTraveled += Vector3.Distance(lastPosition, transform.position);
        }

        if (distanceTraveled > maxTravelDistance)
        {

            Destroy(gameObject);
        }

    }

    public void GetDirectionToGo(Vector3 clickPosition)
    {

        firedByNpc = false;
        fireDirection = (clickPosition - transform.position).normalized;

    }

    public void GetNpcFireDirection(Vector3 playerPosition)
    {

        firedByNpc = true;
        fireDirection = (playerPosition - transform.position).normalized;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("SolidObjects"))
        {

            Destroy(gameObject);

        }

        if (firedByNpc)
        {
            if (collision.gameObject.CompareTag("Player"))
            {

                PlayerStats.takeDamage(damage);
                Destroy(gameObject);

            }
        } else if (!firedByNpc)
        {
            if (collision.gameObject.CompareTag("NPC"))
            {
                Destroy(gameObject);
            }
        }

        
    }

}
