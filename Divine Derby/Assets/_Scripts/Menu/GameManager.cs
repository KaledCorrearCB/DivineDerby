using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private PlayerScore playerScore;
    private bool isGameOver = false;

    private void Start()
    {
        playerScore = FindObjectOfType<PlayerScore>();
    }

    private void Update()
    {
        if (isGameOver) return;

        GasMechanic gas = FindObjectOfType<GasMechanic>();
        if (gas != null && gas.OutOfFuel)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("[GameManager] GAME OVER 🚨");

        // Guardar puntaje
        if (playerScore != null)
        {
            ScoreData.finalScore = playerScore.score;
        }

        //  Cambio de escena 
        Debug.Log("Cambiando a GameOver...");
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    public void YouWin()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("[GameManager] ¡YouWin() llamado correctamente!");

        if (playerScore != null)
        {
            ScoreData.finalScore = playerScore.score;
        }

        SceneManager.LoadScene("YouWin");
    }
}
