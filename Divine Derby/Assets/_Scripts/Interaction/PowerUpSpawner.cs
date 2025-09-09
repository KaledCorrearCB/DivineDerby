using UnityEngine;

using UnityEngine.InputSystem;
public class PowerUpSpawner : MonoBehaviour
{
    [Header("Prefab del power-up")]
    public GameObject powerUpPrefab;

    [Header("Punto de aparición")]
    public Transform spawnPoint;

    [Header("Tiempo de reaparición")]
    public float respawnTime = 5f;

    private GameObject powerUpActual;

    private void Start()
    {
        GenerarPowerUp();
    }

    public void GenerarPowerUp()
    {
        if (powerUpPrefab != null && spawnPoint != null)
        {
            powerUpActual = Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity);

            // Revisamos si es SpeedPill
            SpeedPill pill = powerUpActual.GetComponent<SpeedPill>();
            if (pill != null)
            {
                pill.spawner = this;
            }

            // Revisamos si es MoreGas
            MoreGas gas = powerUpActual.GetComponent<MoreGas>();
            if (gas != null)
            {
                gas.spawner = this;
            }
        }
    }

    // Llamado por el power-up cuando lo recogen
    public void NotificarRecoleccion()
    {
        Invoke(nameof(GenerarPowerUp), respawnTime);
    }
}
