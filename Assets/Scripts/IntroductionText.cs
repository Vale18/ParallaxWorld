using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.VisualScripting;

public class IntroductionText : MonoBehaviour
{   
    [SerializeField][TextArea]
    public List<string> textList;
    public TMP_Text tmpro;
    private InputManagement inputManager;
    private ShowOverlay showOverlay;
    private GameObject other;
    // Start is called before the first frame update
    void Start()
    {
        /*tmpro = GetComponentInChildren<TMP_Text>();*/
        inputManager = FindObjectOfType<InputManagement>();
        showOverlay = FindObjectOfType<ShowOverlay>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.other = other.transform.parent.gameObject;
            ToggleCameraControl();
            StartCoroutine(DelayText());
            showOverlay.IntroOverlayShownDelay();
        }
        
        
    }

    private IEnumerator DelayText()
    {
        foreach (var textblock in textList)
        {
            tmpro.text = textblock;
            yield return new WaitForSeconds(8);
        }
        ToggleCameraControl();
        Destroy(this);
    }
    void ToggleCameraControl()
    {
        var playerController = other.transform.GetComponent<PlayerController>();
        playerController.ToggleMovement();
    }
}
