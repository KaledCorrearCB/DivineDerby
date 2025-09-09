using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointObject : MonoBehaviour
{
    [Header("Puntos que otorga")]
    public int points = 10;

    [HideInInspector] public SpawnerPoint spawner;

    private void OnTriggerEnter(Collider other)
    {
        PlayerScore playerScore = other.GetComponent<PlayerScore>();
        if (playerScore != null)
        {
            playerScore.AddPoints(points);

            // Avisamos al spawner
            if (spawner != null)
                spawner.NotificarRecoleccion();

            Destroy(gameObject);
        }
    }
}
