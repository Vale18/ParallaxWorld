using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Cache = Unity.VisualScripting.Cache;

public class RotateToImage : MonoBehaviour
{
    private GameObject other;
    public float rotationSpeed = 1.0f; // Geschwindigkeit der Rotation
    public Volume postProcessingVolume; // Post-Processing-Volume für Blur-Effekte
    private bool rotateCamera = false;
    private Quaternion targetRotation;
    private MotionBlur motionBlur;

    void Start()
    {
        // Suche nach dem MotionBlur-Effekt im Volume
        if (!postProcessingVolume.profile.TryGet<MotionBlur>(out motionBlur))
        {
            Debug.LogError("MotionBlur effect not found in the post-processing volume.");
        }
    }

    void FixedUpdate()
    {
        if (rotateCamera)
        {
            // Drehe die Kamera zur Zielrotation
            other.transform.rotation = Quaternion.Slerp(other.transform.localRotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            // Wenn die Rotation nahezu abgeschlossen ist, stoppe die Rotation und den Blur
            if (Quaternion.Angle(other.transform.localRotation, targetRotation) < 0.1f)
            {
                other.transform.rotation = targetRotation;
                rotateCamera = false;
                if (motionBlur != null)
                {
                    motionBlur.active = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.other = other.transform.parent.gameObject;
             // Setze die Zielrotation (90 Grad nach links)
            targetRotation = Quaternion.Euler(other.transform.localEulerAngles.x, other.transform.localEulerAngles.y - 90, other.transform.localEulerAngles.z);
            rotateCamera = true;
            if (motionBlur != null)
            {
                motionBlur.active = true; // Aktiviere den Blur-Effekt
            }
            ToggleCameraControl();
        }
    }

    void ToggleCameraControl()
    {
            var cameraMovement = other.transform.GetComponent<CameraMovement>();
            cameraMovement.ToggleMovement();
    }
}