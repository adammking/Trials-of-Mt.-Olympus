using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUIScript : MonoBehaviour
{

    private RawImage youDied;
    private float currentAlpha = 0.0f;
    public float alphaIncreaseRate = 0.0002f;

    private void Awake()
    {
        youDied = GetComponent<RawImage>();
        enabled = false;

    }

    private void Update()
    {
        
        if (currentAlpha >= 1.0f)
        {
            enabled = false;
        } else
        {
            ShowDeathImage();
        }

    }

    public void ShowDeathImage()
    {

        currentAlpha += alphaIncreaseRate;
        youDied.color = new Color(1.0f, 1.0f, 1.0f, currentAlpha);
        
    }



    public void HideDeathImage()
    {
        youDied.color = new Color(1.0f, 1.0f, 1.0f, 0);
        currentAlpha = 0.0f;
    }

    public void EnableObject()
    {
        enabled = true;
    }

    public void DisableObject()
    {
        enabled = false;
    }

}
