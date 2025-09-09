using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPoint : MonoBehaviour
{
    [Header("Prefab del objeto que da puntos")]
    public GameObject pointObjectPrefab;

    [Header("Punto de aparición")]
    public Transform spawnPoint;

    [Header("Tiempo de reaparición")]
    public float respawnTime = 5f;

    private GameObject currentObject;

    private void Start()
    {
        GenerarObjeto();
    }

    public void GenerarObjeto()
    {
        if (pointObjectPrefab != null && spawnPoint != null)
        {
            currentObject = Instantiate(pointObjectPrefab, spawnPoint.position, Quaternion.identity);

            // Pasamos la referencia al spawner
            PointObject obj = currentObject.GetComponent<PointObject>();
            if (obj != null)
            {
                obj.spawner = this;
            }
        }
    }

    public void NotificarRecoleccion()
    {
        Invoke(nameof(GenerarObjeto), respawnTime);
    }
}
