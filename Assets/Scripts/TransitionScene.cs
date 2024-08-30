using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScene : MonoBehaviour
{
    [SerializeField] private int sceneID;
    [SerializeField] private int waitTime = 2;
    void Awake()
    {
        
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
        
        yield return new WaitForSeconds(waitTime);
        targetScript.LoadNextScene(sceneID);
    }
}
