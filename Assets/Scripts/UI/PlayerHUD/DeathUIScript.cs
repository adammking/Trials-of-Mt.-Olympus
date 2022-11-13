using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUIScript : MonoBehaviour
{

    private float currentAlpha = 0.0f;
    public float alphaIncreaseRate;
    private CanvasGroup canvasGroup;

    public delegate void CharacterDead();
    public static CharacterDead characterDead;

    public delegate void CharacterBroughtToLife();
    public static CharacterBroughtToLife characterBroughtToLife;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        enabled = false;
        characterDead += CharacterHasDied;
        characterBroughtToLife += CharacterHasBeenBroughtToLife;

    }

    private void OnEnable()
    {
        characterDead += CharacterHasDied;
        characterBroughtToLife += CharacterHasBeenBroughtToLife;
    }

    private void OnDisable()
    {
        characterDead -= CharacterHasDied;
        characterBroughtToLife -= CharacterHasBeenBroughtToLife;
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

    private void ShowDeathImage()
    {

        currentAlpha += alphaIncreaseRate;
        canvasGroup.alpha = currentAlpha;
        
    }

    private void HideDeathImage()
    {
        canvasGroup.alpha = 0.0f;
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

    public void CharacterHasDied()
    {
        EnableObject();
        ShowDeathImage();
    }

    public void CharacterHasBeenBroughtToLife()
    {
        HideDeathImage();
        DisableObject();
    }

}
