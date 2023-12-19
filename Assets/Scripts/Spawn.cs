using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] enemigosPrefabs;
    public float tiempoEntreSpawn = 5f;
    public int cantidadEnemigos = 10;
    public float radioSpawn = 5f;

    void Start()
    {
        StartCoroutine(SpawnEnemigos());
    }

    IEnumerator SpawnEnemigos()
    {
        for (int i = 0; i < cantidadEnemigos; i++)
        {
            // Calcular posición aleatoria dentro del radio especificado
            Vector2 posicionAleatoria = Random.insideUnitCircle * radioSpawn;
            Vector3 posicionSpawn = new Vector3(posicionAleatoria.x, posicionAleatoria.y, 0f) + transform.position;

            // Elegir un enemigo aleatorio de la lista
            GameObject enemigoPrefab = enemigosPrefabs[Random.Range(0, enemigosPrefabs.Length)];

            // Instanciar enemigo en la posición aleatoria
            Instantiate(enemigoPrefab, posicionSpawn, Quaternion.identity);

            // Esperar antes de generar el siguiente enemigo
            yield return new WaitForSeconds(tiempoEntreSpawn);
        }
    }
}