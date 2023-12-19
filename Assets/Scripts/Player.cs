using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [Header("Velocidad Player")]
    public float moveSpeed = 5f;
    private Transform playerTransform;
    [Header("Bala Player")]
    public GameObject bala ;
    public Transform firepoint;
    public float fireRate = 15f;
    private float nextFireTime = 0.5f;
    [Header ("HP Player")]
    public float vida;
    public float maximoVida;
    public BarraHP barraDeVida;

    private void Start()
    {
        playerTransform = transform;
        vida = maximoVida;
        barraDeVida.inicializarBarraDeVida(vida);
    }

    private void Update()
    {
        // Movimiento
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        playerTransform.position += movement * moveSpeed * Time.deltaTime;

        // Apuntado
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(playerTransform.localPosition);
        Vector2 aimDirection = (mousePosition - screenPoint).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        playerTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Disparo
        if (Input.GetMouseButtonDown(0) && Time.time > nextFireTime)
        {
            AudioManager.Instance.PlaySFX(3);
            Shoot();
            nextFireTime = Time.time + 2f / fireRate;
        }
    }

    // Disparo
    private void Shoot()
    {
        Instantiate (bala, firepoint.position, firepoint.rotation);
    }

    // HP, pausa y cambio de escena

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            AudioManager.Instance.PlaySFX(1);
            vida -= 1f;
            barraDeVida.cambiarVidaActual(vida);
            if (vida <= 0)
            {              
                Time.timeScale = 0f;
                gameObject.SetActive(false);
                SceneManager.LoadScene(2);
            }
        }
    }
}