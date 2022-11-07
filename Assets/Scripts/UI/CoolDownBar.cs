using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownBar : MonoBehaviour
{

    public Slider slider;

    public void SetCoolDownTime(int value)
    {
        slider.maxValue = value;
    }

    public void SetCurrentTime(int value)
    {
        slider.value = value;
    }

}
