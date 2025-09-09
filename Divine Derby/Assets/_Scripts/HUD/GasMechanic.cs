using System;
using UnityEngine;

public class GasMechanic : MonoBehaviour
{
    public float fuel;
    public bool OutOfFuel { get; private set; } = false;

    private void Awake()
    {
        if (fuel < 0f) fuel = 0f;
    }

    // Llamado desde CarMovement; es seguro y no carga escenas.
    public void UpdateTachymeter(Rigidbody rb)
    {
        if (rb == null) return;
        if (OutOfFuel) return;

        if (rb.velocity.magnitude > 0.1f)
        {
            fuel -= Time.deltaTime;
            fuel = Mathf.Max(0f, fuel);

            // Actualización HUD con protecciones
            var hud = HUDManager.Instance;
            if (hud != null && hud.gasNeedle != null)
            {
                hud.NeedleRotation(fuel);
            }
        }

        // Marcar OutOfFuel aquí
        if (fuel <= 0f && !OutOfFuel)
        {
            OutOfFuel = true;
            Debug.Log("[GasMechanic] OutOfFuel = true");
        }
    }

    public void Refuel(float amount)
    {
        if (amount <= 0f) return;

        var hud = HUDManager.Instance;
        float maxFuel = (hud != null) ? hud.maxFuel : float.MaxValue;
        fuel = Mathf.Min(fuel + amount, maxFuel);

        if (hud != null && hud.gasNeedle != null)
            hud.NeedleRotation(fuel);

        if (fuel > 0f) OutOfFuel = false;

        Debug.Log("Gasolina recargada: " + fuel);
    }

}
