using System;
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

    [SerializeField] AudioClip sonidoMoneda, sondioAcelerador, sonidoDecelerador;
    [SerializeField] AudioManager manager;
    
    private int puntuacion;
    private int muertes;

    public Vector3 respawnPosition = new Vector3 (-0.025f,1.277f, -25.351f);

    [SerializeField] TMP_Text textoPuntacion, textoVida, textoMuertes;
    
    [SerializeField] GameObject camaraPrincipal, camaraCenital;

    private float impulsoAcelerador = 2f;

    

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
            if(DetectarSuelo() == true)
            {
                rb.AddForce(0, fuerzaSalto, 0, ForceMode.Impulse);
            }
        }

        if (muertes >= 10)
        {
            SceneManager.LoadScene(3);
        }
        
     
        
    }

    private void TepearASpawn()
    {
        velocidad = 0f;
        transform.position = respawnPosition;
        camaraPrincipal.SetActive(true);
        vida = 100;
        velocidad = 1f;
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
                muertes++;
                textoMuertes.SetText("Muertes: " + muertes);
                TepearASpawn();
               
            }
        }

        if (other.gameObject.CompareTag("VacioMorir"))
        {
            vida = 0;
            muertes++;
            textoMuertes.SetText("Muertes: " + muertes);
            TepearASpawn();
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
        if (other.gameObject.CompareTag("Acelerador"))
        {
            rb.AddForce(0, 0, impulsoAcelerador, ForceMode.Impulse);
            manager.ReproducirSonido(sondioAcelerador);

        }
        if (other.gameObject.CompareTag("Decelerador"))
        {
            rb.AddForce(0, 0, -impulsoAcelerador, ForceMode.Impulse);
            manager.ReproducirSonido(sonidoDecelerador);


        }
        if(other.gameObject.CompareTag("Meta"))
        {
            SceneManager.LoadScene(2);
        }



    }
}
