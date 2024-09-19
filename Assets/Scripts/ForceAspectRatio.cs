using UnityEngine;
using UnityEngine.UI; // Für CanvasScaler

public class ForceAspectRatio : MonoBehaviour
{
    public float targetAspect = 2f / 3f; // Beispiel für ein Hochformat-Seitenverhältnis
    private CanvasScaler canvasScaler;

    void Start()
    {
        // Finde das Canvas mit dem Tag "Overlay"
        GameObject canvasObject = GameObject.FindWithTag("Overlay");
        if (canvasObject != null)
        {
            canvasScaler = canvasObject.GetComponent<CanvasScaler>();
        }
        AdjustAspectRatio();
    }

    void AdjustAspectRatio()
    {
        // Berechnen Sie das aktuelle Seitenverhältnis
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        // Passen Sie die Kamera an, um das gewünschte Seitenverhältnis zu erzwingen
        Camera camera = GetComponent<Camera>();
        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = camera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            camera.rect = rect;
        }

        // Update the Canvas Scaler to match the camera aspect ratio
        if (canvasScaler != null)
        {
            canvasScaler.matchWidthOrHeight = (scaleHeight < 1.0f) ? 1.0f : 0.0f; // Höhe bevorzugen, wenn das Fenster höher als das Zielseitenverhältnis ist, sonst Breite
        }
    }

    void Update()
    {
        AdjustAspectRatio();
    }
}