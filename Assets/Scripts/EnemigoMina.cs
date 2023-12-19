using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoMina : MonoBehaviour
{
    public float velocidad = 2f;
    public int daño = 1;
    public GameObject prefabMoneda;
    public float tiempoDestruccion = 0f;

    void Update()
    {
        // Enemigo sigue al jugador
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");

        if (jugador != null)
        {
            Vector3 direccion = jugador.transform.position - transform.position;
            direccion.z = 0f;
            transform.Translate(direccion.normalized * velocidad * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            // Enemigo impacta al jugador
            JugadorImpactado(otro.GetComponent<Player>());
        }
        else if (otro.CompareTag("Bala Player"))
        {
            // Enemigo impactado por una bala
            BalaImpactada(otro.gameObject);
        }
    }

    void JugadorImpactado(Player jugador)
    {
        // Restar vida al jugador
        BarraHP barraHPJugador = jugador.GetComponentInChildren<BarraHP>();

        if (barraHPJugador != null)
        {
            barraHPJugador.cambiarVidaActual(barraHPJugador.slider.value - daño);
        }
        // Destruir el enemigo
        DestruirEnemigo();
    }
    void BalaImpactada(GameObject bala)
    {
        // Destruir la bala
        Destroy(bala);

        // Soltar monedas
        SoltarMonedas();

        // Destruir el enemigo
        DestruirEnemigo();
        AudioManager.Instance.PlaySFX(2);
    }

    void SoltarMonedas()
    {
        // Instanciar monedas
        if (prefabMoneda != null)
        {
            Instantiate(prefabMoneda, transform.position, Quaternion.identity);
        }
    }

    void DestruirEnemigo()
    {
        // Destruir el enemigo después de un tiempo
        Destroy(gameObject, tiempoDestruccion);
    }
}
