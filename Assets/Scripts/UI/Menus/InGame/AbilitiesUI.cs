using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class AbilitiesUI : MonoBehaviour
{
    
    public TextMeshProUGUI abilityOneText;
    public TextMeshProUGUI abilityTwoText;
    public TextMeshProUGUI abilityOneDescription;
    public TextMeshProUGUI abilityTwoDescription;
    public TMP_Dropdown abilityOneDropDown;
    public TMP_Dropdown abilityTwoDropDown;

    public PlayerAttacks playerAttacks;
    public GameObject managers;
    public DataManager dataManager;
    public AbilityBook abilityBook;

    private List<Ability> abilities;
    private int dropOneSelectValue = 0;
    private int dropTwoSelectValue = 0;
    private List<string> abilityOneSelection;
    private List<string> abilityTwoSelection;

    private UnityAction<int> DropDownOneChanged;
    private UnityAction<int> DropDownTwoChanged;

    private void Awake()
    {
        abilityBook = managers.GetComponent<AbilityBook>();
        dataManager = managers.GetComponent<DataManager>();
        abilities = abilityBook.Abilities;

    }


    private void OnEnable()
    {
        DropDownOneChanged += AbilityOneChanged;
        DropDownTwoChanged += AbilityTwoChanged;
        
    }

    private void OnDisable()
    {
        DropDownOneChanged -= AbilityOneChanged;
        DropDownTwoChanged -= AbilityTwoChanged;
    }

    void Start()
    {

        ReorderAbilities();

        AddListeners();

    }


    private void ReorderAbilities()
    {
        abilityOneText.text = playerAttacks.abilityOneInfo.Name;
        abilityOneDescription.text = playerAttacks.abilityOneInfo.Description;
        abilityTwoText.text = playerAttacks.abilityTwoInfo.Name;
        abilityTwoDescription.text = playerAttacks.abilityTwoInfo.Description;
        abilityOneSelection = new();
        abilityTwoSelection = new();
        int collectionOneIndex = 0;
        int collectionTwoIndex = 0;
        foreach (Ability ability in abilities)
        {
            if (ability.Name == playerAttacks.abilityOneInfo.Name)
            {
                dropOneSelectValue = collectionOneIndex;
                abilityOneSelection.Add(ability.Name);
            }
            else if (ability.Name == playerAttacks.abilityTwoInfo.Name)
            {
                dropTwoSelectValue = collectionTwoIndex;
                abilityTwoSelection.Add(ability.Name);
            }
            else
            {
                collectionOneIndex++;
                collectionTwoIndex++;
                abilityOneSelection.Add(ability.Name);
                abilityTwoSelection.Add(ability.Name);
            }

        }
        abilityOneDropDown.ClearOptions();
        abilityOneDropDown.AddOptions(abilityOneSelection);
        abilityTwoDropDown.ClearOptions();
        abilityTwoDropDown.AddOptions(abilityTwoSelection);
        abilityOneDropDown.value = dropOneSelectValue;
        abilityTwoDropDown.value = dropTwoSelectValue;
    }

    private void AbilityOneChanged(int value)
    {
        foreach (Ability ability in abilities)
        {
            if (ability.Name == abilityOneSelection[value])
            {
              
                playerAttacks.ChangeAbilityOne(ability);
                break;

            }
        }
        RemoveListeners();
        ReorderAbilities();
        AddListeners();
    }

    private void AbilityTwoChanged(int value)
    {
        foreach (Ability ability in abilities)
        {
            if (ability.Name == abilityTwoSelection[value])
            {
                playerAttacks.ChangeAbilityTwo(ability);
                break;
            }
        }
        RemoveListeners();
        ReorderAbilities();
        AddListeners();
    }


    private void RemoveListeners()
    {
        abilityOneDropDown.onValueChanged.RemoveListener(DropDownOneChanged);
        abilityTwoDropDown.onValueChanged.RemoveListener(DropDownTwoChanged);
    }

    private void AddListeners()
    {
        abilityOneDropDown.onValueChanged.AddListener(DropDownOneChanged);
        abilityTwoDropDown.onValueChanged.AddListener(DropDownTwoChanged);
    }


}
