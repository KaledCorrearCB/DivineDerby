using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class SpeedPill : MonoBehaviour
{
    [Header("Speed pill")]
    public float multiplier = 1.5f;   // Factor de multiplicación de velocidad
    public float duration = 5f;      // Duración del efecto

    private static bool isBoostActive = false;
    private static Coroutine activeCoroutine;

    [HideInInspector] public PowerUpSpawner spawner; 

    private void OnTriggerEnter(Collider car)
    {
        CarMovement carMovement = car.GetComponent<CarMovement>();
        if (carMovement == null) return;

        CarSO carSO = carMovement.car;

        if (isBoostActive && activeCoroutine != null)
        {
            carMovement.StopCoroutine(activeCoroutine);
            carSO.speed = carMovement.OriginalSpeed;
        }

        activeCoroutine = carMovement.StartCoroutine(SpeedUpdate(carSO, carMovement));

        Destroy(gameObject);

        // Se avisa que se recoge la pastilla
        if (spawner != null)
            spawner.NotificarRecoleccion();
    }

    IEnumerator SpeedUpdate(CarSO carSO, CarMovement carMovement)
    {
        isBoostActive = true;

        if (carMovement.OriginalSpeed == 0)
            carMovement.OriginalSpeed = carSO.speed;

        carSO.speed = carMovement.OriginalSpeed * multiplier;

        yield return new WaitForSeconds(duration);

        carSO.speed = carMovement.OriginalSpeed;
        isBoostActive = false;
        activeCoroutine = null;
    }

}
