using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxStamina(int value)
    {
        slider.maxValue = value;
    }

    public void SetStamina(int value)
    {
        slider.value = value;
    }

}
