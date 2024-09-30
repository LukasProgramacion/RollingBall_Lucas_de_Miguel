using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjemploVectores : MonoBehaviour
{


    [SerializeField] Vector3 direccion = new Vector3(2, 0, 0);
    [SerializeField] int velocidad = 5;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        

        
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;

        if (timer > 0 && timer < 5f)
        {
            transform.Translate(direccion.normalized * velocidad * Time.deltaTime);
        }
        else
        {
            direccion = direccion * -1f;
            timer = 0f;
        }
       
        
        
        
    }

    // 
    
}
