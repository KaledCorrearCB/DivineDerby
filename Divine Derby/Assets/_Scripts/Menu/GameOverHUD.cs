using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameOverHUD : MonoBehaviour
{
    public string escenaInicial = "Menu";
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = "Puntuación final: " + ScoreData.finalScore;
    }


    public void Reiniciar()
    {
        SceneManager.LoadScene(escenaInicial);
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}

