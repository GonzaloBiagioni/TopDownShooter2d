using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Moneda : MonoBehaviour
{
    // Numero entero de la moneda
    public int value;
    void OnTriggerEnter2D(Collider2D Coin)
    {
        if (Coin.CompareTag("Player"))
        {
            Destroy(gameObject);
            MonedaContador.Instance.IncreaseCoin(value);
        }
    }
}