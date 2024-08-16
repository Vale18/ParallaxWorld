using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagement : MonoBehaviour
{
    public delegate void MovementInputHandler(Vector2 input);
    public event MovementInputHandler OnMovementInput;
    public delegate void InteractionInputHandler(float input);
    public event InteractionInputHandler OnInteractionInput;
    public void OnMovement(InputValue context)
    {
        Vector2 movementInput = context.Get<Vector2>();
        Debug.Log("Movement Input: " + movementInput);
        OnMovementInput?.Invoke(movementInput);
    }

    public void OnInteraction(InputValue context)
    {
        float interactionInput = context.Get<float>();
        OnInteractionInput?.Invoke(interactionInput);
    }
}