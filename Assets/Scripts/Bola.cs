using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{
    [SerializeField] Vector3 direccion = new Vector3 (0, 0, 0);
    [SerializeField] float velocidad;
    [SerializeField] float fuerzaSalto, fuerzaX;
    [SerializeField] int vida;
    private float h;
    private float v; 

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        h = Input.GetAxisRaw("Horizontal"); // h = 1 (D ó ->), h = -1 (A ó <-), h = 0 (NADA) 

        v = Input.GetAxisRaw("Vertical"); // v = 1 (W ó ^), v = -1 (S ó v), v = 0 (NADA) 
                                        //direccion = new Vector3(h, 0, v);

        // ESTO ES MOVIMIENTO CON KINEMATIC NO RESPETA FISICA SE JODEN COSAS
        //transform.position += new Vector3(h, 0, v) * velocidad * Time.deltaTime;





        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, fuerzaSalto, 0, ForceMode.Impulse);
        }
        
        
        
    }
        
    private void FixedUpdate()
    {
        rb.AddForce(h, 0, v, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Colecionable")
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Trampa"))
        {
            vida -= 10;
            if (vida <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        
    }
}
