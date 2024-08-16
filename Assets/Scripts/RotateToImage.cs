using System;
using System.Collections;
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
    private InputManagement inputManager;
    public GameObject eyeOutline, pupil;
    void Start()
    {
        // Suche nach dem MotionBlur-Effekt im Volume
        if (!postProcessingVolume.profile.TryGet<MotionBlur>(out motionBlur))
        {
            Debug.LogError("MotionBlur effect not found in the post-processing volume.");
        }
            inputManager = FindObjectOfType<InputManagement>();
            
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
                rotateCamera = false;
                other.transform.rotation = targetRotation;
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
            SetEye(false);
            TeleportToCenter();
            ToggleCameraControl();
            RotateCamera(-90f);
            inputManager.OnInteractionInput += ReturnToNormal;
        }
    }

    void TeleportToCenter()
    {
        Vector3 centerPos = other.transform.position;
        centerPos.x = transform.parent.localPosition.x;
        centerPos.z = transform.parent.localPosition.z;
        other.transform.position = centerPos;
    }
    private void OnTriggerExit(Collider other)
    {
        SetEye(true);
    }

    void SetEye(bool set)
    {
        eyeOutline.SetActive(set);
        pupil.SetActive(set);
    }
    void RotateCamera(float rotationValue)
    {
        rotateCamera = true;
        // Setze die Zielrotation (90 Grad nach links)
        targetRotation = Quaternion.Euler(other.transform.localEulerAngles.x, other.transform.localEulerAngles.y  + rotationValue, other.transform.localEulerAngles.z);
        if (motionBlur != null)
        {
            motionBlur.active = true; // Aktiviere den Blur-Effekt
        }
    }
    void ToggleCameraControl()
    {
            var cameraMovement = other.transform.GetComponent<CameraMovement>();
            cameraMovement.ToggleMovement();
    }

    private void ReturnToNormal(float press)
    {
        if (!rotateCamera)
        {
            inputManager.OnInteractionInput -= ReturnToNormal;
            RotateCamera(90f);
            StartCoroutine(DelayUnlockCameraControl());
        }
    }

    private IEnumerator DelayUnlockCameraControl()
    {
        yield return new WaitForSeconds(5);
        ToggleCameraControl();
    }
}