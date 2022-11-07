using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownManager : MonoBehaviour
{

    public static CooldownManager instance;

    private List<Ability> abilitiesOnCooldown = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (abilitiesOnCooldown.Count > 0)
        {
            for (int i = 0; i < abilitiesOnCooldown.Count; i++)
            {
                abilitiesOnCooldown[i].CurrentCooldown -= Time.deltaTime;

                if (abilitiesOnCooldown[i].CurrentCooldown <= 0)
                {
                    abilitiesOnCooldown[i].CurrentCooldown = 0;
                    abilitiesOnCooldown.Remove(abilitiesOnCooldown[i]);
                }
            }
        }
        
    }

    public void StartCooldown(Ability ability)
    {

        if (!abilitiesOnCooldown.Contains(ability))
        {
            ability.CurrentCooldown = ability.CoolDown;
            abilitiesOnCooldown.Add(ability);
        }

    }

}
