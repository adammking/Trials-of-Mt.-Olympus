using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    public Slider slider;

    public delegate void SetMaxHP(int value);
    public static SetMaxHP setMaxHP;

    public delegate void SetHP(int value);
    public static SetHP setHP;

    private void Awake()
    {
        setMaxHP = SetMaxHealth;
        setHP = SetHealth;
    }

    public void SetMaxHealth(int value)
    {
        slider.maxValue = value;
    }

    public void SetHealth(int value)
    {
        slider.value = value;
    }

}
