using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bola : MonoBehaviour
{
    [SerializeField] Vector3 direccion = new Vector3 (0, 0, 0);
    [SerializeField] float velocidad;
    [SerializeField] float fuerzaSalto, fuerzaX;
    [SerializeField] int vida;
    private float h;
    private float v;

    [SerializeField] float distanciaDeteccionSuelo;
    [SerializeField] LayerMask queEsSuelo;

    [SerializeField] AudioClip sonidoMoneda;
    [SerializeField] AudioManager manager;
    
    private int puntuacion;
    
    [SerializeField] TMP_Text textoPuntacion, textoVida;
    
    [SerializeField] GameObject camaraPrincipal, camaraCenital;

    

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        h = Input.GetAxisRaw("Horizontal"); // h = 1 (D � ->), h = -1 (A � <-), h = 0 (NADA) 

        v = Input.GetAxisRaw("Vertical"); // v = 1 (W � ^), v = -1 (S � v), v = 0 (NADA) 
                                        //direccion = new Vector3(h, 0, v);

        // ESTO ES MOVIMIENTO CON KINEMATIC NO RESPETA FISICA SE JODEN COSAS
        //transform.position += new Vector3(h, 0, v) * velocidad * Time.deltaTime;





        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(DetectarSuelo() == true)
            {
                rb.AddForce(0, fuerzaSalto, 0, ForceMode.Impulse);
            }
        }
        
        
        
    }

    bool DetectarSuelo()
    {
        bool resultado = Physics.Raycast(transform.position, new Vector3(0, -1, 0), distanciaDeteccionSuelo, queEsSuelo);
        return resultado;
    }
        
    private void FixedUpdate()
    {
        rb.AddForce(h, 0, v, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Colecionable"))
        {
            manager.ReproducirSonido(sonidoMoneda);
            puntuacion += 5;
            textoPuntacion.SetText("Puntacion: " + puntuacion);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Trampa"))
        {
            vida -= 10;
            textoVida.SetText("Vida: " + vida);
            if (vida <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (other.gameObject.CompareTag("VacioMorir"))
        {
            vida = 0;
            SceneManager.LoadScene(1);
        }

        if (other.gameObject.CompareTag("ParedCambiadora"))
        {
            camaraPrincipal.SetActive(false);
            camaraCenital.SetActive(true);

        }
        if (other.gameObject.CompareTag("ParedCambiadora2"))
        {
            camaraCenital.SetActive(false);
            camaraPrincipal.SetActive(true);
            

        }


    }
}
