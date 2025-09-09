using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    // Variables necesarias
    private Rigidbody _rb;
    private InputManager _inputM;
    private GasMechanic gasMechanic;
    public float OriginalSpeed { get; set; }

    // Variable del Scriptable Object del carro.
    [Header("Values")]
    public CarSO car;
    private float _steeringAngle;
 
    // Asignacion de las variables de los componentes de colisiones de las llantas.
    [Header("Wheels")]
    [SerializeField] private WheelCollider frontRightCollider;
    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider backRightCollider;
    [SerializeField] private WheelCollider backLeftCollider;

    // Asignacion de las variables de los componentes de Transform de las llantas.
    [SerializeField] private Transform frontRightPosition;
    [SerializeField] private Transform frontLeftPosition;
    [SerializeField] private Transform backRightPosition;
    [SerializeField] private Transform backLeftPosition;

    private void Awake()
    {
        // Se almacena el componente en la variable
        _rb = GetComponent<Rigidbody>();
        _inputM = GetComponent<InputManager>();

        // Cambia el centro de gravedad del carro
        _rb.centerOfMass = new Vector3(0, -1f, 0);
        gasMechanic = GetComponent<GasMechanic>();
    }



    private void FixedUpdate()
    {
        Motor();
        UpdateWheels();
        Steering();
    }


    private void Update()
    {
        kmhHud();
        GasolinaHUD();
    }


    public void kmhHud()
    {
        float speed = _rb.velocity.magnitude;

        // Actualiza HUD con la velocidad
        HUDManager.Instance.KmByHour(speed);


    }

    public void GasolinaHUD()
    {
        if (gasMechanic == null) return;
        if (gasMechanic.OutOfFuel) return; // ya no seguir llamando cuando se quedó sin gas
        gasMechanic.UpdateTachymeter(_rb);
    }

    public void Motor()
    {

        // SI no hay gasolina entonces el carro no puede moverse
        if (gasMechanic.fuel <= 0)
        {
            frontLeftCollider.motorTorque = 0;
            frontRightCollider.motorTorque = 0;
            backLeftCollider.motorTorque = 0;
            backRightCollider.motorTorque = 0;

            return; //Para que no se aplique movimiento
        }


        // Asignar al torque de motor el input vertical por la velocidad.
        frontLeftCollider.motorTorque = _inputM.input.y * car.speed;
        frontRightCollider.motorTorque = _inputM.input.y * car.speed;
        backLeftCollider.motorTorque = _inputM.input.y * car.speed;
        backRightCollider.motorTorque = _inputM.input.y * car.speed;
    }
        // Método para configurar el freno.
    public void Brake(InputAction.CallbackContext context)
    {
        // Si se presiona el botón, la fuerza de frenado tiene un valor, si no, queda en cero.
        if (context.performed)
        {
            frontLeftCollider.brakeTorque = car.brakeForce;
            frontRightCollider.brakeTorque = car.brakeForce;
            backLeftCollider.brakeTorque = car.brakeForce;
            backRightCollider.brakeTorque = car.brakeForce;
            
        } else if (context.canceled)
        {
            frontLeftCollider.brakeTorque = 0;
            frontRightCollider.brakeTorque = 0;
            backLeftCollider.brakeTorque = 0;
            backRightCollider.brakeTorque = 0;
        }
    }

        //Método que aplica el giro en las ruedas.
    public void Steering()
    {
        _steeringAngle = car.angle * _inputM.input.x;
        frontLeftCollider.steerAngle = _steeringAngle;
        frontRightCollider.steerAngle = _steeringAngle;
    }
    
        // Método que permite actualizar el transform con el movimiento del collider.
    public void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftCollider, frontLeftPosition);
        UpdateSingleWheel(frontRightCollider, frontRightPosition);
        UpdateSingleWheel(backLeftCollider, backLeftPosition);
        UpdateSingleWheel(backRightCollider, backRightPosition);
    }
   
    public void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        
        // Se obtiene la posición y rotación del collider.
        wheelCollider.GetWorldPose(out pos, out rot);
        
        // Se aplican estos datos al transform.
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }


}
