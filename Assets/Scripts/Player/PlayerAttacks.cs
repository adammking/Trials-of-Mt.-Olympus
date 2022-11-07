using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{

    public LayerMask solidObjectsLayer;
    public GameObject cantCastThere;
    public GameObject damageZone;
    public int coolDownTime;
    public int abilityLength;

    private bool abilityActive = false;
    private bool coolDownActive = false;
    private float lastTimeAbilityWasUsed;
    private float timeSinceCooldownBarTick;

    private GameObject instantiatedObject;
    private CoolDownBar coolDownBar;
    private GameObject player;

    private void Awake()
    {

        player = GameObject.Find("Player");
        coolDownBar = GameObject.Find("CoolDownBar").GetComponent<CoolDownBar>();
        coolDownBar.SetCoolDownTime(coolDownTime);
        coolDownBar.SetCurrentTime(coolDownTime);

    }

    private void Update()
    {

        if (abilityActive)
        {
            if(Time.time - lastTimeAbilityWasUsed >= abilityLength)
            {
                Destroy(instantiatedObject);
                abilityActive = false;
            }
        }

        if (coolDownActive)
        {
            updateCoolDownBar();
        }

    }

    void updateCoolDownBar()
    {

    }

    public void SpawnDamageZone()
    {
        if (!CheckCoolDownActive() && !abilityActive)
        {
            var spotClicked = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spotClicked.z = 0.0f;

            if (canCastSpell(spotClicked))
            {
                instantiatedObject = Instantiate(damageZone, spotClicked, new Quaternion());
                abilityActive = true;
                coolDownActive = true;
                lastTimeAbilityWasUsed = Time.time;
            } else
            {
                print("Can't cast there.");
                instantiatedObject = Instantiate(cantCastThere, spotClicked, new Quaternion());
            }
            
        }

    }

    private bool CheckCoolDownActive()
    {
        
        if((Time.time <= coolDownTime && !coolDownActive) || (Time.time - lastTimeAbilityWasUsed >= coolDownTime && !abilityActive))
        {
            coolDownActive = false;
            return false;
        }

        return true;
    }

    private bool canCastSpell(Vector3 clickPosition)
    {

        // Checks for solid objects between cast location and player location
        if(Physics2D.Linecast(player.transform.position, clickPosition, solidObjectsLayer))
        {
            return false;
        }

        return true;

    }

}
