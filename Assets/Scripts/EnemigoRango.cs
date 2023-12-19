using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoRango : MonoBehaviour
{
    public float velocidadMovimiento = 3f;
    public float rangoDisparo = 5f;
    public int vida = 3;
    public GameObject balaEnemigo;
    public Transform puntoDeDisparo;
    public GameObject prefabMoneda;
    public float tiempoEsperaEntreDisparos = 2f;

    private float tiempoUltimoDisparo;

    void Start()
    {
        tiempoUltimoDisparo = Time.time;
    }

    void Update()
    {
        // Enemigo sigue al jugador
        MoverHaciaJugador();

        // Realizar disparo si ha pasado el tiempo de espera
        if (Time.time - tiempoUltimoDisparo > tiempoEsperaEntreDisparos)
        {
            Disparar();
            tiempoUltimoDisparo = Time.time;
        }
    }

    void MoverHaciaJugador()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");

        if (jugador != null)
        {
            // Calcular la dirección hacia el jugador
            Vector3 direccion = jugador.transform.position - transform.position;
            float distanciaAlJugador = direccion.magnitude;

            // Si está fuera del rango de disparo, moverse hacia el jugador
            if (distanciaAlJugador > rangoDisparo)
            {
                transform.Translate(direccion.normalized * velocidadMovimiento * Time.deltaTime, Space.World);
            }
            else
            {
                // Si está en rango de disparo, rotar gradualmente hacia el jugador
                Quaternion rotacionHaciaJugador = Quaternion.LookRotation(Vector3.forward, direccion);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionHaciaJugador, velocidadMovimiento * Time.deltaTime);
            }
        }
    }

    void Disparar()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");

        if (jugador != null)
        {
            // Calcular la dirección hacia el jugador
            Vector3 direccion = jugador.transform.position - transform.position;
            float distanciaAlJugador = direccion.magnitude;

            // Si está en rango de disparo, disparar desde el punto de origen
            if (distanciaAlJugador <= rangoDisparo)
            {
                Vector3 posicionDisparo = puntoDeDisparo != null ? puntoDeDisparo.position : transform.position;
                Instantiate(balaEnemigo, posicionDisparo, transform.rotation);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Bala Player"))
        {
            // Restar vida al enemigo por cada bala recibida
            vida--;

            if (vida <= 0)
            {
                // Si el enemigo queda sin vida, soltar monedas y destruirlo
                SoltarMonedas();
                Destroy(gameObject);
            }

            // Destruir la bala
            Destroy(otro.gameObject);
        }
    }

    void SoltarMonedas()
    {
        if (prefabMoneda != null)
        {
            Instantiate(prefabMoneda, transform.position, Quaternion.identity);
        }
    }
}