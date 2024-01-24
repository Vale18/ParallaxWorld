using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTextbox : MonoBehaviour
{
    GameObject textbox;
    PlayQuickSound sound;
    AudioSource audioSource =  null;
    // Start is called before the first frame update
    void Start()
    {
        textbox = transform.Find("Canvas").gameObject;
        textbox.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player entered trigger");
            textbox.SetActive(true);
            audioSource.Play();
        }

    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player left trigger");
            StartCoroutine(DeactivateAfterDelay());
        }
    }
    IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(3);
        textbox.SetActive(false);
    }
}
