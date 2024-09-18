using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private InputManagement inputManager;
    
    // Start is called before the first frame update
    void Start()
    {
        inputManager = FindObjectOfType<InputManagement>();
        inputManager.OnStartInput += LoadFirstScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }
}
