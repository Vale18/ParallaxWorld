using UnityEngine;

public class FloatAround : MonoBehaviour
{
    // Bewegungseinstellungen
    public float moveSpeed = 2f; // Geschwindigkeit der Auf- und Abbewegung
    public float moveAmplitude = 1f; // Amplitude der Bewegung (wie weit es sich bewegt)

    // Rotationseinstellungen
    public float rotateSpeed = 50f; // Geschwindigkeit der Rotation um die Z-Achse

    private Vector3 initialPosition; // Anfangsposition des GameObjects
    private float time;

    void Start()
    {
        // Speichere die Anfangsposition des GameObjects
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Inkrementiere die Zeit basierend auf der festen Zeitstufe
        time += Time.fixedDeltaTime;

        // Berechne die neue Position für die Auf- und Abbewegung
        Vector3 newPosition = initialPosition;
        newPosition.y += Mathf.Sin(time * moveSpeed) * moveAmplitude;

        // Setze die neue Position
        transform.position = newPosition;

        // Führe die Rotation um die Z-Achse durch
        transform.Rotate(Vector3.forward, rotateSpeed * Time.fixedDeltaTime);
    }
}