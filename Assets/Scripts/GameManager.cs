using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void CambioEscena(string nombre)
    {
        // Reiniciar el juego
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        // Ir al men� principal
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0); 
    }

    public void quitgame()
    {
        Application.Quit();
    }
}
