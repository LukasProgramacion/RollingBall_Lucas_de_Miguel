using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource SFX;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReproducirSonido(AudioClip clip)
    {
       //Ejectua el clip
        SFX.PlayOneShot(clip);
    }

}
