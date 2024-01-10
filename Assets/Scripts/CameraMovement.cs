using UnityEngine;
using UnityEngine.InputSystem;
 
public class CameraMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f; // Grundgeschwindigkeit der Kamera
    private Vector2 movementInput;
 
    // void OnEnable()
    // {
    //     // Registriere die Bewegungsaktion
    //     var gamepad = Gamepad.current;
    //     if (gamepad != null)
    //     {
    //         gamepad.leftStick.performed += ctx => OnMovement(ctx);
    //         gamepad.leftStick.canceled += ctx => OnMovement(ctx);
    //     }
    // }
 
    // void OnDisable()
    // {
    //     // Deregistriere die Bewegungsaktion
    //     var gamepad = Gamepad.current;
    //     if (gamepad != null)
    //     {
    //         gamepad.leftStick.performed -= ctx => OnMovement(ctx);
    //         gamepad.leftStick.canceled -= ctx => OnMovement(ctx);
    //     }
    // }
 
    void OnMovement(InputValue context)
    {
        // Aktualisiere die Bewegungseingabe
        movementInput = context.Get<Vector2>();
        Debug.Log("Movement Input: " + movementInput);
    }
 
    void Update()
    {
        // Bewege die Kamera basierend auf der Joystick-Eingabe
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y);
        transform.Translate(movement * movementSpeed * Time.deltaTime);
    }
}