using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraYAdjustment : MonoBehaviour
{
    public float yOffset = 0f; // Distance the camera's Y position will adjust relative to the player
    public float smoothSpeed = 0.1f; // Speed of the smooth transition
    private Camera mainCamera;
    private bool adjustY = false;
    private Transform playerTransform;
    private bool lastPos = false;
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
        // Smoothly adjust the camera's Y position if the player is on the tile
        if (adjustY && playerTransform != null)
        {
            Vector3 cameraPosition = mainCamera.transform.position;
            float targetY = playerTransform.position.y + yOffset;
            cameraPosition.y = Mathf.Lerp(cameraPosition.y, targetY, smoothSpeed);
            cameraPosition.y = Mathf.RoundToInt(cameraPosition.y);
            mainCamera.transform.position = cameraPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player steps on the tile
        if (collision.CompareTag("Player") && !lastPos)
        {
            playerTransform = collision.transform;
            lastPos = true;
            adjustY = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Stop adjusting the camera's Y position when the player leaves the tile
        if (collision.CompareTag("Player") && lastPos)
        {
            adjustY = false;
            lastPos = false;
        }
    }
}

