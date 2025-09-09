
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    // Variables que almacenan los datos para las barras de valores.
    [Header("Car Values")]
    public Slider speedSB;
    public Slider brakeSB;
    public Slider angleSB;
    public float maxSpeed;
    public float maxBrake;
    public float maxAngle;

    // Lista en donde se almacenan los carros que se van a manejar.
    public CarSO[] carList = new CarSO[3];

    // Variable que almacena cual es el carro seleccionado.
    private CarSO selectedCar;

    // Variable que almacena una posición en el espacio para que el carro aparezca.
    public Transform spawnCarPosition;

    // Variables de la interfaz gráfica para que varien dependiendo del carro elegido.
    [SerializeField] private Image carImage;
    [SerializeField] private TextMeshProUGUI carName;

    // Índice para recorrer el arreglo.
    private int carIndex;
    // Variable para poner el timer
    public GameTimer gameTimer;

    private void Awake()
    {
        // El índice se inicializa en 0.
        carIndex = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        // El carro seleccionado es el primer carro del arreglo.
        selectedCar = carList[carIndex];

        // Se llama el método para actualizar los datos de la pantalla.
        UpdateSelection();
    }

    public void UpdateSelection()
    {
        // Cuando se efectúa el método, la imagen de la interfaz se actualiza con la del carro seleccionado.
        carImage.sprite = selectedCar.carImage;

        // Cuando se efectúa el método, el texto de la interfaz se actualiza con el del carro seleccionado.
        carName.text = selectedCar.carName;

        // Se llama el método que actualiza las barras con los datos.
        SetScrollBars();
    }

    // Método que se llama con el botón.
    public void ChosenCar()
    {
        // Se instancia el prefab del carro seleccionado y se almacena en una variable.
        var chosenCar = Instantiate(selectedCar.carPrefab, spawnCarPosition.position, Quaternion.identity);

        // Se le indica a la cámara que el objetivo es el carro seleccionado.
        CameraController.instance.target = chosenCar.transform;

        // Se guarda la instancia dentro de una ubicación en el inspector.
        chosenCar.transform.parent = spawnCarPosition.transform;
        // Se activa el HUD y el tiempo inmediatamente despues de selccionar el carro
        HUDManager.Instance.SetHUDActive(true);
        if (gameTimer != null)
        {
            gameTimer.StartTimer(); // o gameTimer.StartTimer(60f) si quieres pasar un valor explícito
        }
        else
        {
            Debug.LogWarning("GameTimer no asignado en CarSelection");
        }
        // Codigo para que empiece la musica de juego
        MusicManager.Instance.PlayGameMusic();
    }

    // Método para cambiar el carro seleccionado.
    public void ChangeCarRight()
    {
        // Valida que si el indice es menor al máximo de carros, que suba, y con ello sube el carro de la lista.
        // luego el carro seleccionadon cambia y se aplican los cambios en la interfaz.

        carIndex = (carIndex + 1) % carList.Length;
        selectedCar = carList[carIndex];
        UpdateSelection();

    }

    public void ChangeCarLeft()
    {
        carIndex = (carIndex - 1 + carList.Length) % carList.Length;
        selectedCar = carList[carIndex];
        UpdateSelection();
    }

    // Método que actualiza los valores de las barras.
    void SetScrollBars()
    {
        // A cada barra se le da un valor de 0 - 1, dividiento los valores de cada carro con su máximo.
        speedSB.value = Mathf.Clamp01(selectedCar.speed / maxSpeed);
        brakeSB.value = Mathf.Clamp01(selectedCar.brakeForce / maxBrake);
        angleSB.value = Mathf.Clamp01(selectedCar.angle / maxAngle);

    }
}
