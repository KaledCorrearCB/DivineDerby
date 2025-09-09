using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicloDiaNoche : MonoBehaviour
{
    [Range(0.0f, 24f)] public float Hora = 12;

    [Header("Duración del día en minutos")]
    public float DuracionDelDíaEnMinutos = 5;

    [Header("Sol (Directional Light)")]
    public Transform Sol;

    [Header("Intensidades personalizadas")]
    public float IntensidadNoche = 0.06f;
    public float IntensidadMediodia = 2f;

    private Light luzSol;
    private float SolX;

    private void Start()
    {
        if (Sol != null)
        {
            luzSol = Sol.GetComponent<Light>();
        }
    }

    private void Update()
    {
        // Avanzar la hora
        Hora += Time.deltaTime * (24 / (60 * DuracionDelDíaEnMinutos));
        if (Hora >= 24) Hora = 0;

        RotacionSol();
        ActualizarIntensidad();
    }

    void RotacionSol()
    {
        SolX = 15 * Hora; // 360° / 24h = 15° por hora
        Sol.localEulerAngles = new Vector3(SolX, 0, 0);
    }

    void ActualizarIntensidad()
    {
        if (luzSol == null) return;

        // Convierte la hora a un ángulo (0h = 0°, 12h = 180°, 24h = 360°)
        float angulo = (Hora / 24f) * 360f;

        // Curva senoidal suave (valores entre -1 y 1)
        float curva = Mathf.Cos(angulo * Mathf.Deg2Rad);

        // Normalizamos a 0-1
        curva = Mathf.Clamp01(curva);

        // Interpolamos entre noche y mediodía
        float intensidad = Mathf.Lerp(IntensidadNoche, IntensidadMediodia, curva);

        luzSol.intensity = intensidad;
    }
}
