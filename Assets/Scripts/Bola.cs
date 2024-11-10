using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bola : MonoBehaviour
{
    [SerializeField] Vector3 direccion = new Vector3 (0, 0, 0);
    [SerializeField] float fuerzaSalto, fuerzaX;
    [SerializeField] int vida;
    private float h;
    private float v;

    [SerializeField] float distanciaDeteccionSuelo;
    [SerializeField] LayerMask queEsSuelo;

    [SerializeField] AudioClip sonidoMoneda, sondioAcelerador, sonidoDecelerador, sonidoPerderVida, sonidoMorir, sonidoRespawnear;
    [SerializeField] AudioManager manager;
    
    private int puntuacion;
    private int muertes;

    public Vector3 respawnPosition = new Vector3 (-0.025f,1.277f, -25.351f);

    [SerializeField] TMP_Text textoPuntacion, textoVida, textoMuertes, textoResumen;
    
    [SerializeField] GameObject camaraPrincipal, camaraCenital;

    private float impulsoAcelerador = 2f;

    public Image blackScreen;
    public float velocidadFadeOut = 0.01f;



    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //textoResumen.gameObject.SetActive(false);
        
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
            manager.ReproducirSonido(sonidoMorir);
            SceneManager.LoadScene(3);
        }

        Color color = blackScreen.color;
        color.a -= velocidadFadeOut;

        if (color.a <= 0)
        {
            color.a = 0;
            blackScreen.gameObject.SetActive(false);
        }

        blackScreen.color = color;



    }

    private void TepearASpawn()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        transform.position = respawnPosition;

        rb.isKinematic = false;
        camaraPrincipal.SetActive(true);
        
        manager.ReproducirSonido(sonidoRespawnear);
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
            textoPuntacion.SetText(": " + puntuacion);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Trampa"))
        {
            vida -= 10;
            textoVida.SetText(": " + vida);
            manager.ReproducirSonido(sonidoPerderVida);
            if (vida <= 0)
            {
                muertes+=2;
                textoMuertes.SetText(": " + muertes);
                
                TepearASpawn();
                vida = 100;
                textoVida.SetText(": " + vida);


            }
        }

        if (other.gameObject.CompareTag("VacioMorir"))
        {
            vida -= 10;
            textoVida.SetText(": " + vida);
            TepearASpawn();
            muertes++;
            textoMuertes.SetText(": " + muertes);
            if (vida <= 0)
            {
                muertes += 2;
                textoMuertes.SetText(": " + muertes);

                TepearASpawn();
                vida = 100;
                textoVida.SetText(": " + vida);


            }
            
            
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
            /*
            DontDestroyOnLoad(textoMuertes);
            textoResumen.gameObject.SetActive(true);
            textoResumen.SetText("ACABASTE CON: " + puntuacion + "PUNTOS Y: " +  muertes + " MUERTES");
            */
        }



    }
}
