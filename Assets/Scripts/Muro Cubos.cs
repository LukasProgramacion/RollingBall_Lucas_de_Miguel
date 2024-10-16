using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuroCubos : MonoBehaviour
{
    [SerializeField] private Rigidbody[] rbs;

    private float timer = 0f;
    private bool iniciarCuenta = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
       
        if (iniciarCuenta)
        {
            timer += 1 * Time.unscaledDeltaTime;
            if (timer >= 2)
            {
                Time.timeScale = 1f;
                for (int i = 0; i < rbs.Length; i++)
                {
                    rbs[i].useGravity = true;
                }
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0.1f;
            iniciarCuenta = true;
        }
    }
}
