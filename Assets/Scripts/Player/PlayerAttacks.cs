using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{

    public LayerMask solidObjectsLayer;
    public GameObject cantCastThere;
    public GameObject managers;

    public GameObject abilityOne;
    public Ability abilityOneInfo;
    public GameObject abilityTwo;
    public Ability abilityTwoInfo;
       
   
    private DataManager dataManager;
    private CooldownManager coolDownManager;

    private void Awake()
    {

        dataManager = managers.GetComponent<DataManager>();
        coolDownManager = managers.GetComponent<CooldownManager>();

        abilityOne = (GameObject)Resources.Load("Prefabs/Abilities/FrostPatch", typeof(GameObject));
        abilityTwo = (GameObject)Resources.Load("Prefabs/Abilities/Fireball", typeof(GameObject));
        abilityOneInfo = dataManager.LoadAbilityData("FrostPatch");
        abilityTwoInfo = dataManager.LoadAbilityData("Fireball");

    }

    public void ChangeAbilityOne(Ability ability)
    {
        abilityOne = LoadAbilityPrefab(ability.Code);
        abilityOneInfo = LoadAbilityInfo(ability.Code);
    }

    public void ChangeAbilityTwo(Ability ability)
    {
        abilityTwo = LoadAbilityPrefab(ability.Code);
        abilityTwoInfo = LoadAbilityInfo(ability.Code);
    }

    public Ability LoadAbilityInfo(string abilityName)
    {
        return dataManager.LoadAbilityData(abilityName);
    }

    public GameObject LoadAbilityPrefab(string prefabName)
    {

        return (GameObject)Resources.Load($"Prefabs/Abilities/{prefabName}", typeof(GameObject));

    }

    public void CastAbility1()
    {
        if (abilityOneInfo.CurrentCooldown <= 0)
        {
            CastSpell(abilityOne, abilityOneInfo);
        }
    }

    public void CastAbility2()
    {
        if (abilityTwoInfo.CurrentCooldown <= 0)
        {
            CastSpell(abilityTwo, abilityTwoInfo);
        }
    }

    public void CastSpell(GameObject ability, Ability abilityInfo)
    {
        var spotClicked = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spotClicked.z = 0.0f;

        if (abilityInfo.IsAoe)
        {
           
            if (CanCastLosSpell(spotClicked))
            {
                Instantiate(ability, spotClicked, new Quaternion());
                coolDownManager.StartCooldown(abilityInfo);

            }
            else
            {
                Instantiate(cantCastThere, spotClicked, new Quaternion());
            }

        } 
        else
        {
            var boltAbility = Instantiate(ability, transform.position, new Quaternion());
            boltAbility.GetComponent<IAbility>().GetDirectionToGo(spotClicked);
            coolDownManager.StartCooldown(abilityInfo);
        }
        
    }

    private bool CanCastLosSpell(Vector3 clickPosition)
    {

        // Checks for solid objects between cast location and player location
        if(Physics2D.Linecast(transform.position, clickPosition, solidObjectsLayer))
        {
            return false;
        }

        return true;

    }

}
