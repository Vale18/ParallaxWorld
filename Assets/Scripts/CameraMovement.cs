using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public float movementSpeed = .5f;
    private Vector2 movementInput;
    private float timeSinceLastInput = 0.0f;
    private bool isAutoMoving = false;
    public bool isSidescroller = true;

/*     void OnEnable()
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            gamepad.leftStick.performed += ctx => OnMovement(ctx);
            gamepad.leftStick.canceled += ctx => OnMovement(ctx);
        }
    }

    void OnDisable()
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            gamepad.leftStick.performed -= ctx => OnMovement(ctx);
            gamepad.leftStick.canceled -= ctx => OnMovement(ctx);
        }
    } */

    void OnMovement(InputValue context)
    {
        movementInput = context.Get<Vector2>();
        Debug.Log("Movement Input: " + movementInput);
        timeSinceLastInput = 0.0f;
        isAutoMoving = false;
    }

    void FixedUpdate()
    {
        timeSinceLastInput += Time.deltaTime;

        // Automatische Bewegung nach 5 Sekunden Inaktivität
        if (timeSinceLastInput > 5.0f)
        {
            StartAutoMovement();
        }
        else
        {
            PerformManualMovement();
        }
    }

    void StartAutoMovement()
    {
        isAutoMoving = true;
        // Bewegung nach hinten und rechts
        Vector3 autoMovement;
        if(isSidescroller)
        {
            autoMovement = new Vector3(1, 0, 0).normalized;
        }
        else
        {
            autoMovement = new Vector3(0, 0, 1).normalized;
        }
        transform.Translate(autoMovement * movementSpeed * .6f * Time.deltaTime);
    }

    void PerformManualMovement()
    {
        if (isAutoMoving) return; // Verhindere manuelle Bewegung während der automatischen Bewegung

        if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
        {
            movementInput.y = 0;
        }
        else
        {
            movementInput.x = 0;
            
        }  
        Vector3 movement = new Vector3(movementInput.x*2, 0, movementInput.y*3);
        transform.Translate(movement * movementSpeed * Time.deltaTime);
    }

    public void ToggleMovement()
    {
        var rb = GetComponent<Rigidbody>();
        if ((rb.constraints & RigidbodyConstraints.FreezePosition) == RigidbodyConstraints.FreezePosition)
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePosition;
        }
    }
}
