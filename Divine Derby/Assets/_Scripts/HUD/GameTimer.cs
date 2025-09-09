using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [Header("Tiempo de partida (segundos)")]
    public float startTime = 60f; 
    private float currentTime;
    private bool isGameOver = false;
    private bool timerRunning = false; //Bool que nos ayuda a indicar cuando empezar a correr el tiempo

    private void Start()
    {
        currentTime = startTime;
        isGameOver = false;
        timerRunning = false; // importante: no iniciar automáticamente
        if (HUDManager.Instance != null)
            HUDManager.Instance.UpdateTimerUI(currentTime); // actualiza la UI (aunque esté oculta)
    }

    private void Update()
    {
        if (!timerRunning || isGameOver) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            GameOver();
        }

        if (HUDManager.Instance != null)
            HUDManager.Instance.UpdateTimerUI(currentTime);


    }

    public void StartTimer(float time = -1f)
    {
        if (time > 0f) currentTime = time;
        else currentTime = startTime;

        isGameOver = false;
        timerRunning = true;
        if (HUDManager.Instance != null)
            HUDManager.Instance.UpdateTimerUI(currentTime);
    }

    public void ResetTimer()
    {
        timerRunning = false;
        isGameOver = false;
        currentTime = startTime;
        if (HUDManager.Instance != null)
            HUDManager.Instance.UpdateTimerUI(currentTime);
    }
    private void GameOver()
    {
        timerRunning = false;
        isGameOver = true;
        Debug.Log("Juego terminado");

        // Avisar al GameManager
        var gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.YouWin(); // 👈 Aquí lo mandamos a la escena "YouWin"
        }
        else
        {
            Debug.LogError("[GameTimer] No se encontró un GameManager en la escena");
        }

    }

}
