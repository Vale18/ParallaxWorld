using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5.0f; // Geschwindigkeit der Kamerabewegung

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Horizontaler Input (links/rechts Pfeiltasten)
        float verticalInput = Input.GetAxis("Vertical");     // Vertikaler Input (oben/unten Pfeiltasten)

        // Berechne die neue Position der Kamera
        Vector3 newPosition = transform.position + new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime;

        // Aktualisiere die Position der Kamera
        transform.position = newPosition;
    }
}
