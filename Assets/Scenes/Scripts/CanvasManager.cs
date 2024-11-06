using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
   public void EmpezarPartida()
    {
        //Cargar escena 1
        SceneManager.LoadScene(1);
    }

    public void TerminarJuego()
    {
        Application.Quit();
    }

    public void HojaInstrucciones()
    {
        SceneManager.LoadScene(4);
    }

}
