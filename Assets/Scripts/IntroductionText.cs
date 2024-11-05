using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroductionText : MonoBehaviour
{   
    [SerializeField][TextArea]
    public List<string> textList;
    public TMP_Text tmpro;
    private InputManagement inputManager;
    private ShowOverlay showOverlay;
    private GameObject other;
    private int currentTextIndex = 0; // Index Textblock
    private Coroutine textCoroutine; 

    // Start is called before the first frame update
    void Start()
    {
        inputManager = FindObjectOfType<InputManagement>();
        showOverlay = FindObjectOfType<ShowOverlay>();
        
    }

    private void ContinueText(float press = 0)
    {
        if (textCoroutine != null)
        {
            StopCoroutine(textCoroutine); 
        }
        
        // Start Coroutine to show next text
        textCoroutine = StartCoroutine(ShowText());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.other = other.transform.parent.gameObject;
            ToggleCameraControl();
            textCoroutine = StartCoroutine(ShowText());
            showOverlay.IntroOverlayShownDelay();
            inputManager.OnInteractionInput += ContinueText;

        }
    }

    private IEnumerator ShowText()
    {
        if (currentTextIndex < textList.Count)
        {
            tmpro.text = textList[currentTextIndex];
            currentTextIndex++;
            // delay to read text
            yield return new WaitForSeconds(11);
            ContinueText();
        }
        else
        {
            ToggleCameraControl();
            Destroy(this);
        }
    }

    void ToggleCameraControl()
    {
        var playerController = other.transform.GetComponent<PlayerController>();
        playerController.ToggleMovement();
    }
}
