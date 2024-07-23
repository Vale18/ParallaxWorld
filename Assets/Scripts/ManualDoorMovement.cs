using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class ManualDoorMovement : MonoBehaviour
{
    private BoxCollider collider;
    private CameraMovement cameraMovement;
    private VideoPlayer videoPlayerForForestDoor;
    public float cameraMoveTime = 12f;
    private float cameraMoveSpeed;
    private float targetZPosition;
    private bool isMovingCamera = false;
    public RenderTexture videoRenderer;
    private GameObject forestDoorObj;
    private InputManagement inputManager;
    void Awake()
    {
        // Zugriff auf die Komponenten
        collider = GetComponentInChildren<BoxCollider>();
        cameraMovement = GetComponent<CameraMovement>();
        forestDoorObj = GameObject.FindGameObjectWithTag("ForrestDoor");
        videoPlayerForForestDoor = forestDoorObj.GetComponent<VideoPlayer>();
        inputManager = FindObjectOfType<InputManagement>();
        inputManager.OnInteractionInput += Interact;
        // Deaktivieren der Komponenten
        ToggleManualMode();

    }



    void Update()
    {
        if (isMovingCamera)
        {
            MoveCamera();
        }
    }

    void ToggleManualMode()
    {
        if (collider != null)
            collider.isTrigger = !collider.isTrigger;

        if (cameraMovement != null)
            cameraMovement.enabled = !cameraMovement.enabled;
    }
   
    public void Interact(float press)
    {
        if (!isMovingCamera && collider.isTrigger)
        {
            forestDoorObj.transform.GetChild(0).gameObject.SetActive(false); // Deaktiviere Video Preview
            videoPlayerForForestDoor.Play();
            StartCameraMove();
        }
    }
    [ContextMenu("Trigger OnInteraction")]
    void DebugOnInteraction()
    {
        Interact(1f);
    }

    void StartCameraMove()
    {
        targetZPosition = transform.position.z + 1f;
        cameraMoveSpeed = (targetZPosition - transform.position.z) / cameraMoveTime;
        isMovingCamera = true;
    }

    void MoveCamera()
    {
        // Bewegen der Kamera
        float newZ = transform.position.z + cameraMoveSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

        // Prüfen, ob die Bewegung abgeschlossen ist
        if ((cameraMoveSpeed > 0 && newZ >= targetZPosition) || (cameraMoveSpeed < 0 && newZ <= targetZPosition))
        {
            isMovingCamera = false;
            ToggleManualMode();  // Aktivieren Sie die Komponenten wieder, nachdem die Bewegung abgeschlossen ist
            forestDoorObj.SetActive(false);
        }
    }
}

