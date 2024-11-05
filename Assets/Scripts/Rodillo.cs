using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rodillo : MonoBehaviour
{
    [SerializeField] float fuerzaRodillo;
    [SerializeField] Vector3 direccionRotacion;
    
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddTorque(direccionRotacion * fuerzaRodillo, ForceMode.VelocityChange);
        
        

    }

    // Update is called once per frame
    void Update()
    {
        //Rotate(new Vector3(0, 1, 0) * velocidadRotacion * Time.deltaTime, Space.World);
        

    }

    private void FixedUpdate()
    {
        
    }
}
