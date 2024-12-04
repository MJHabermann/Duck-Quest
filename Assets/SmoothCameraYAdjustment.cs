using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraYAdjustment : MonoBehaviour
{
    public Vector3 targetCameraPosition; // The exact position to move the camera to
    public float smoothSpeed =10.0f; // Speed of the smooth transition
    private Camera mainCamera;
    private bool moveCamera = false;

    private void Start()
    {
        // Find the main camera in the scene
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
        }
    }

    private void Update()
    {
        // Smoothly move the camera to the target position if triggered
        if (moveCamera)
        {
            Vector3 currentCameraPosition = mainCamera.transform.position;
            mainCamera.transform.position = Vector3.Lerp(currentCameraPosition, targetCameraPosition, smoothSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player steps on the tile
        if (collision.CompareTag("Player"))
        {
            moveCamera = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Stop moving the camera when the player leaves the tile
        if (collision.CompareTag("Player"))
        {
            moveCamera = false;
        }
    }

}

