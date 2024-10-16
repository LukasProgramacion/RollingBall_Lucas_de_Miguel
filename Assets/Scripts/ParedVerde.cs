using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedVerde : MonoBehaviour
{
    [SerializeField] Vector3 direccionGiro = new Vector3(0, 5, 0), direccion = new Vector3(0, 0.5f, 0);
    [SerializeField] float velocidadGiro = 7, velocidad;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        if (timer < 0.5f)
        {
            transform.Rotate(direccionGiro * velocidadGiro * Time.deltaTime);
            transform.Translate(direccion.normalized * velocidad * Time.deltaTime, Space.World);
        }
        else
        {
            direccion = direccion * -1;
            timer = 0;
        }
    }
}
