using System.Collections;
using UnityEngine;

public class ShowOverlay : MonoBehaviour
{
    private InputManagement inputManager;
    private Canvas canvas;
    private Coroutine inactivityCoroutine;

    void Start()
    {
        inputManager = FindObjectOfType<InputManagement>();
        canvas = GetComponent<Canvas>();
        inputManager.OnMovementInput += HandleMovementInput;
        inputManager.OnInteractionInput += PrimaryPressed;
        canvas.enabled = false;
    }
    void PrimaryPressed(float input)
    {
            canvas.enabled = false;
    }

    public void IntroOverlayShownDelay()
    {
        inactivityCoroutine = StartCoroutine(InactivityCheck(31));
    }
    public void RotateScreenOverlayShownDelay()
    {
        inactivityCoroutine = StartCoroutine(InactivityCheck(4));
    }
    void HandleMovementInput(Vector2 input)
    {
        if (input.magnitude < 0.01f)
        {
            if (inactivityCoroutine == null)
            {
                inactivityCoroutine = StartCoroutine(InactivityCheck());
            }
        }
        else
        {
            if (inactivityCoroutine != null)
            {
                StopCoroutine(inactivityCoroutine);
                inactivityCoroutine = null;
            }
            if (canvas.enabled)
            {
                canvas.enabled = false;
            }
        }
    }

    private IEnumerator InactivityCheck()
    {
        
        yield return new WaitForSeconds(5);
        canvas.enabled = true;
    }
    private IEnumerator InactivityCheck(float time)
    {
        inputManager.OnMovementInput -= HandleMovementInput;
        yield return new WaitForSeconds(time);
        inputManager.OnMovementInput += HandleMovementInput;
        canvas.enabled = true;
    }

    void OnDestroy()
    {
        inputManager.OnMovementInput -= HandleMovementInput;
    }
}