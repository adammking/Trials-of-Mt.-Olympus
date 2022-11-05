using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUIScript : MonoBehaviour
{

    private RawImage youDied;
    private void Awake()
    {
        youDied = GetComponent<RawImage>();
    }

    public void ShowDeathImage()
    {
        youDied.color = new Color(youDied.color.r, youDied.color.g, youDied.color.b, 255);
        
    }

    public void HideDeathImage()
    {
        youDied.color = new Color(youDied.color.r, youDied.color.g, youDied.color.b, 0);
    }

}
