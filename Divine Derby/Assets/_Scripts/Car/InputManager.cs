
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Variables necesarias
    [HideInInspector] public Vector2 input;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        // Se almacena en la variable el valor del Input System
        input = _playerInput.actions["Move"].ReadValue<Vector2>();
        
    }

}
