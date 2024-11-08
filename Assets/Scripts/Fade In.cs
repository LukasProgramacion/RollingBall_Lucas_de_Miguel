using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image blackScreen;
    public float velocidadFadeOut = 0.01f;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color color = blackScreen.color;
        color.a -= velocidadFadeOut;

        if(color.a <= 0)
        {
            color.a = 0;
            blackScreen.gameObject.SetActive(false);
        }

        blackScreen.color = color;
    }
}
