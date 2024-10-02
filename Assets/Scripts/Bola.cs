using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{
    [SerializeField] Vector3 direccion = new Vector3 (0, 0, 0);
    [SerializeField] float velocidad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // h = 1 (D ó ->), h = -1 (A ó <-), h = 0 (NADA) 
        float v = Input.GetAxisRaw("Vertical"); // v = 1 (W ó ^), v = -1 (S ó v), v = 0 (NADA) 

        
            //direccion = new Vector3(h, 0, v);

            transform.position += new Vector3(h, 0, v) * velocidad * Time.deltaTime;
        
        
    }
}
