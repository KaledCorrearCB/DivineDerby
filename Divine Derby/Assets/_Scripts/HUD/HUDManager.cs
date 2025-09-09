using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    
    [Header("Kilometers")]
    public TextMeshProUGUI kmText;
    public float maxFuel;
    public GameObject gasNeedle;

    [Header("Gas")] 
    public float minRotation = -70f;
    public float maxRotation = 70f;
    // Se pone  una variable nueva con la cual se pueda guardar el texto
    [Header("Score")]
    public TextMeshProUGUI scoreText;
    // Variable para texto tiempo
    [Header("Timer")]
    public TextMeshProUGUI timerText;
    //Variables para empezar o terminar el tiempo
    private bool timerRunning = false;
    private float currentTime;
    // Poner el HUD a desactivar
    public GameObject hudPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        // Se desactiva el HUD al comiendo para que no aparezca durante el momento del menu
        // Se usa una condicional que revise si existe el hud panel para que no presente errores a futuro
        if (hudPanel != null)
        {
            hudPanel.SetActive(false);
        }
    }
    private void Update()
    {
        if (timerRunning)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerUI(currentTime);
            }
            else
            {
                currentTime = 0;
                timerRunning = false;

                var gm = FindObjectOfType<GameManager>();
                if (gm != null)
                {
                    gm.YouWin();
                }
                else
                {
                    Debug.LogError("[HUDManager] No encontré un GameManager en la escena");
                }

            }
        }
    }


    // Método para escribir en el HUD los Km/h. Para ello se multiplica la velocidad por 3.6,
    public void KmByHour(float value)
    {
        var kmByHour = value * 3.6f;
        kmText.text = kmByHour.ToString("####") + " Km/h";
    }
    
    public void NeedleRotation(float fuel)
    {
        // Rota la aguja dependiendo del nivel de gasolina que tenga el carro.
        float fuelPercent = fuel / maxFuel;
        float angle = Mathf.Lerp(minRotation, maxRotation, fuelPercent);
        gasNeedle.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
    // se crea el metodo que escribe el puntaje
    public void UpdateScore(int newScore)
    {
        scoreText.text = "Puntos: " + newScore;
    }

    //Aqui se actualiza el tiempo en el HUD in game
    public void UpdateTimerUI(float timeValue)
    {
        int minutes = Mathf.FloorToInt(timeValue / 60);
        int seconds = Mathf.FloorToInt(timeValue % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //Metodo para inicializar el tiempo del juego
    public void StartTimer(float startTime)
    {
        currentTime = startTime;
        timerRunning = true;
        UpdateTimerUI(currentTime);

    }

    // Metodo para activar o desactivar el HUD panel. 
    public void SetHUDActive(bool state)
    {
        if (hudPanel != null)
        {
            hudPanel.SetActive(state);
        }
    }

}
