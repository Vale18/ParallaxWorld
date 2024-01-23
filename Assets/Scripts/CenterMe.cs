using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterMe : MonoBehaviour
{
    private BoxCollider triggerCollider;
    void Awake()
    {
        triggerCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Beispielhaft: Trigger nur beim Betreten eines bestimmten Tags
        {
            // Berechnen Sie die Mitte des BoxColliders
            Vector3 colliderCenter = triggerCollider.bounds.center;

            // Setzen Sie die Kamera auf die berechnete Position
            Camera.main.transform.position = colliderCenter;
        }
    }
}
