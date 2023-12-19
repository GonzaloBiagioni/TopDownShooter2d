using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float tiempoDeEspera = 60f; 
    private float tiempoRestante;
    public TMP_Text textoContador; 

    void Start()
    {
        tiempoRestante = tiempoDeEspera;
    }

    void Update()
    {
        // Restar tiempo
        tiempoRestante -= Time.deltaTime;

        // Actualizar el contador en el objeto TMP
        textoContador.text = "Tiempo restante: " + Mathf.Ceil(tiempoRestante);

        // Verificar si el tiempo ha llegado a cero
        if (tiempoRestante <= 0f)
        {
            // Cambiar de escena cuando el tiempo llega a cero
            CambiarEscena();
        }
    }

    void CambiarEscena()
    {
        // Cambio de escena
        SceneManager.LoadScene(3);
    }
}