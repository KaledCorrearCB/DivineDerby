using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // El panel del men� de pausa
    private bool isPaused = false;

    void Update()
    {
        // Detectar si se presiona Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);  // Mostrar el men�
        Time.timeScale = 0f;          // Congelar el juego
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Ocultar el men�
        Time.timeScale = 1f;          // Reanudar el juego
        isPaused = false;
    }
}
