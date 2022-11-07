using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttackScript : MonoBehaviour
{

    public GameObject managers;
    public GameObject ability;
    public LayerMask solidObjects;
    public float timeTillLeaveCombat;
    public float detectionTime;
    public float timeSinceDetected;

    private DataManager dataManager;
    private CooldownManager cooldownManager;
    private Ability abilityInfo;

    private NPCBehavior npcBehavior;
    public PlayerInput playerInput;
    public PlayerBehavior playerBehavior;

    private Vector3 playerLastKnownLocation;
    private float timeSinceSeenPlayer;
    private bool playerVisible;

    private void Awake()
    {

        cooldownManager = managers.GetComponent<CooldownManager>();
        dataManager = managers.GetComponent<DataManager>();
        npcBehavior = GetComponent<NPCBehavior>();

    }

    private void Update()
    {

        if (!playerVisible && npcBehavior.InCombat)
        {

            if (Time.time - timeSinceSeenPlayer >= timeTillLeaveCombat)
            {
                print("Leaving combat");
                npcBehavior.LeaveCombat();
            }

        }

    }

    private void Start()
    {

        abilityInfo = dataManager.LoadAbilityData("Frostbolt");
        playerVisible = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!Physics2D.Linecast(transform.position, collision.transform.position, solidObjects))
            {
                playerVisible = true;
                npcBehavior.EnterCombat();
            } 
        }
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
           
            if (Physics2D.Linecast(transform.position, collision.transform.position, solidObjects))
            {
                if (npcBehavior.InCombat)
                {
                    FireAtLastKnownLocation(collision);
                }
                
            }
            else
            {
                FireAtVisibleEnemy(collision);
            }
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerVisible)
        {
            playerLastKnownLocation = collision.transform.position;
            timeSinceSeenPlayer = Time.time;
            playerVisible = false;
        }

    }

    public void FireAtLastKnownLocation(Collider2D collision)
    {
        if (playerVisible)
        {
            playerLastKnownLocation = collision.transform.position;
            timeSinceSeenPlayer = Time.time;
        }
        playerVisible = false;
        if (abilityInfo.CurrentCooldown == 0)
        {
            var boltAbility = Instantiate(ability, transform.position, new Quaternion());
            boltAbility.GetComponent<IAbility>().GetNpcFireDirection(playerLastKnownLocation);
            cooldownManager.StartCooldown(abilityInfo);
        }
    }

    public void FireAtVisibleEnemy(Collider2D collision)
    {
        playerVisible = true;
        if (!npcBehavior.InCombat) npcBehavior.EnterCombat();
        if (abilityInfo.CurrentCooldown == 0)
        {
            var boltAbility = Instantiate(ability, transform.position, new Quaternion());
            var newPosition = PredictedPosition(collision.transform.position, transform.position, playerBehavior.MoveSpeed * Time.deltaTime * playerInput.input, 20.0f * Time.deltaTime);
            // Adujust for feet hitbox
            newPosition.y -= playerBehavior.feetHitboxOffset;
            boltAbility.GetComponent<IAbility>().GetNpcFireDirection(newPosition);
            cooldownManager.StartCooldown(abilityInfo);
        }
    }


    private Vector3 PredictedPosition(Vector2 targetPosition, Vector2 shooterPosition, Vector2 targetVelocity, float projectileSpeed)
    {
        Vector2 displacement = targetPosition - shooterPosition;
        float targetMoveAngle = Vector3.Angle(-displacement, targetVelocity) * Mathf.Deg2Rad;
        //if the target is stopping or if it is impossible for the projectile to catch up with the target (Sine Formula)
        if (targetVelocity.magnitude == 0 || targetVelocity.magnitude > projectileSpeed && Mathf.Sin(targetMoveAngle) / projectileSpeed > Mathf.Cos(targetMoveAngle) / targetVelocity.magnitude)
        {
            Debug.Log("Position prediction is not feasible.");
            return targetPosition;
        }
        //also Sine Formula
        float shootAngle = Mathf.Asin(Mathf.Sin(targetMoveAngle) * targetVelocity.magnitude / projectileSpeed);
        return targetPosition + targetVelocity * displacement.magnitude / Mathf.Sin(Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / targetVelocity.magnitude;
    }

}
