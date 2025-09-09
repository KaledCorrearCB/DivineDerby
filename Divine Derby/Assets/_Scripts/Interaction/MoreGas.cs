using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoreGas : MonoBehaviour
{
    public float refillAmount = 30f;

    [HideInInspector] public PowerUpSpawner spawner; // ← referencia al spawner

    private void OnTriggerEnter(Collider other)
    {
        GasMechanic gas = other.GetComponent<GasMechanic>();
        if (gas != null)
        {
            gas.Refuel(refillAmount);
            Destroy(gameObject);

            // Avisamos al spawner que se recolectó
            if (spawner != null)
                spawner.NotificarRecoleccion();
        }
    }
}
