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
            MySceneManager targetScript = FindObjectOfType<MySceneManager>();
            StartCoroutine(delayLoad(targetScript));
        }
    }
    IEnumerator delayLoad(MySceneManager targetScript)
    {
        
        yield return new WaitForSeconds(2);
        targetScript.LoadChurchScene();
    }
}
