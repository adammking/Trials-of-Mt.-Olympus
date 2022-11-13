using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBook : MonoBehaviour
{

    private List<Ability> abilities = new();
    public List<Ability> Abilities
    {
        get { return abilities; }
    }
    private DataManager dataManager;

    private void Awake()
    {
        dataManager = GetComponent<DataManager>();
        CollectAbilities();

    }

    public void CollectAbilities()
    {
        string path = $"{Application.dataPath}/Data/Abilities/";
        string[] files = Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            
            Ability ability = dataManager.LoadAbilityDataFullPath(file);
            abilities.Add(ability);
            
        }

    }

}
