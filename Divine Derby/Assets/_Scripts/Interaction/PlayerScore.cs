using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    // Puntaje 
    public int score = 0;

    public void AddPoints(int amount)
    {
        score += amount;

        // Actualizamos HUD si existe
        if (HUDManager.Instance != null)
        {
            HUDManager.Instance.UpdateScore(score);
        }

        Debug.Log("Nuevo puntaje: " + score);
    }
}

