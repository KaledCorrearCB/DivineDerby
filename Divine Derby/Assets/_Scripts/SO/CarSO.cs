using UnityEngine;

// Permite crear el objeto en la carpeta de proyecto.
[CreateAssetMenu(fileName ="New Car", menuName = "Car/New Car")]
public class CarSO : ScriptableObject
{
    // Variables que debe tener el objeto.
    public Sprite carImage;
    public string carName;
    public float speed;
    public float brakeForce;
    public float angle;
    public GameObject carPrefab;

}
