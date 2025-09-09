using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // M�todo para el bot�n de Jugar
    public void Jugar(string nombreEscena)
    {
        SceneManager.LoadScene("Car Game");
    }

    // M�todo para el bot�n de Salir
    public void Salir()
    {
        Debug.Log("Saliendo del juego..."); // Funciona en el editor
        Application.Quit(); // Funciona en el build
    }
}
