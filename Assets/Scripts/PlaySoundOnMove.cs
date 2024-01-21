using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class PlaySoundOnMove : MonoBehaviour
{
    // The AudioSource component that will play the sound
    AudioSource audioSource =  null;
    private Vector2 movementInput;
    // The minimum distance the object must move before the sound is played
    public float minMoveDistance = 0.1f;
    // The position of the object in the previous frame
    private Vector3 previousPosition;

    private float timeSinceLastInput = 0.0f;
    private bool isAutoMoving = true;


    private void Awake()
    {
        audioSource = GetComponents<AudioSource>()[1];
    }
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

     void OnMovement(InputValue context)
    {
        movementInput = context.Get<Vector2>();
        timeSinceLastInput = 0.0f;
        isAutoMoving = false;
        Debug.Log("Movement Input: " + movementInput);
    }

    void Update()
    {   
        float moveDistance = Vector3.Distance(transform.position, previousPosition);
        timeSinceLastInput += Time.deltaTime;

        if (moveDistance > 0.00000000000001 && !isAutoMoving && timeSinceLastInput < 1) //
        {
            if (!audioSource.isPlaying) { audioSource.Play(); }
            previousPosition = transform.position;
        }
        else
        {
            audioSource.Stop();
        }
    }
}