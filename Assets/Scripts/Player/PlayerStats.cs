using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStats : MonoBehaviour
{

    private int _maxHealth = 100;
    private int _currentHealth = 100;
    private int _maxStamina = 100;
    private int _currentStamina = 100;
    private bool isAlive = true;
    private bool inCombat = false;

    public int MaxHealth
    {
        get { return _maxHealth; }
        protected set { _maxHealth = value; }
    }
    public int CurrentHealth
    {
        get { return _currentHealth; }
        protected set { _currentHealth = value; }
    }
    public int MaxStamina
    {
        get { return _maxStamina; }
        protected set { _maxStamina = value; }
    }
    public int CurrentStamina
    {
        get { return _currentStamina; }
        protected set {_currentStamina = value; }
    }

    public bool IsAlive
    {
        get { return isAlive; }
        protected set { isAlive = value; }
    }

    public bool InCombat
    {
        get { return inCombat; }
        protected set { inCombat = value; }
    }

    public PlayerHealthBar healthBar;

    public delegate void TakeDamage(int damage);
    public static TakeDamage takeDamage;

    public delegate void TakeHeal(int heal);
    public static TakeHeal takeHeal;

    public delegate void BringToLife();
    public static BringToLife bringToLife;

    void Awake()
    {
        healthBar = healthBar.GetComponent<PlayerHealthBar>();
        UpdateHealthBarMaxHealth();
        UpdateHealthBar();
    }

    private void OnEnable()
    {
        takeDamage += ApplyDamage;
        takeHeal += AddHealth;
        bringToLife += BringCharacterToLife;
    }

    public void OnDisable()
    {
        takeDamage -= ApplyDamage;
        takeHeal -= AddHealth;
        bringToLife -= BringCharacterToLife;
    }

    public void SetHealth(int value)
    {
        
        if (isAlive)
        {
            _currentHealth = value;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

            UpdateHealthBar();

            if (_currentHealth <= 0)
            {
                CharacterDeath();
            }
        }

    }

    public void AddHealth(int value)
    {
        if (_currentHealth == _maxHealth)
        {
            return;
        }

        if (isAlive)
        {
            _currentHealth += value;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

            UpdateHealthBar();
        }
        
    }

    public void SetMaxHealth(int value)
    {
        _maxHealth = value;
        UpdateHealthBarMaxHealth();

    }

    public void ApplyDamage(int damageAmount)
    {
        if (isAlive)
        {
            _currentHealth -= damageAmount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

            UpdateHealthBar();

            if (_currentHealth <= 0)
            {
                CharacterDeath();
            }
        }
    }

    public void SetStamina(int value)
    {
        _currentStamina = value;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
    }
    public void SetMaxStamina(int value)
    {
        _maxStamina = value;
    }

    public void ReduceStamina(int value)
    {
        _currentStamina -= value;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
    }

    private void UpdateHealthBar()
    {
        healthBar.SetHealth(_currentHealth);
    }

    private void UpdateHealthBarMaxHealth()
    {
        healthBar.SetMaxHealth(_currentHealth);
    }

    private void CharacterDeath()
    {
        if (isAlive)
        {
            print("You're dead. RIP.");
            isAlive = false;
            DeathUIScript.characterDead();
            Time.timeScale = 0.0f;
        }
        
    }

    public void BringCharacterToLife()
    {
        if (!isAlive)
        {
            print("Welcome back from the dead.");
            isAlive = true;
            SetHealth(_maxHealth);
            DeathUIScript.characterBroughtToLife();
            Time.timeScale = 1.0f;
        }
        else
        {
            print("You're already alive.");
        }
        
    }

}
