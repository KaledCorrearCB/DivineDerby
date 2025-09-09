using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeZone : MonoBehaviour
{
    [Range(0f, 1f)] public float slowMultiplier = 0.5f; // cuánto se reduce la velocidad
    public float slowDuration = 3f; // tiempo en segundos que dura el efecto

    private void OnTriggerEnter(Collider other)
    {
        var rb = other.GetComponentInParent<Rigidbody>();
        if (rb != null)
        {
            StartCoroutine(ApplySlow(rb));
        }

        Debug.Log("Me tocaste la pua");
    }

    private IEnumerator ApplySlow(Rigidbody rb)
    {
        // Reducimos la velocidad actual del rigidbody directamente
        rb.velocity = rb.velocity * slowMultiplier;

        // Guardamos drag original
        float originalDrag = rb.drag;

        // Aumentamos el drag para que el carro pierda velocidad más rápido
        rb.drag = originalDrag + 2f;

        yield return new WaitForSeconds(slowDuration);

        // Restauramos el drag
        rb.drag = originalDrag;
    }
}
