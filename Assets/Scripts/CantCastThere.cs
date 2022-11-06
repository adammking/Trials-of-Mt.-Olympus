using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CantCastThere : MonoBehaviour
{
    private float maxOpacityLevel = 1.0f;
    public float opacDecIncrement;

    public SpriteRenderer spriteRenderer;

    void Update()
    {
        if (maxOpacityLevel <= 0)
        {
            Destroy(gameObject);
        }
       
        ChangeOpacity();

    }

    void ChangeOpacity()
    {
        maxOpacityLevel -= opacDecIncrement;
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, maxOpacityLevel);

    }

}
