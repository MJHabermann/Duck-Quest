using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerAttachToPlayerY : MonoBehaviour
{
    private Camera mainCamera;
    private Transform playerTransform;
    private bool isAttachedToPlayer = false;

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
        // If attached to the player, follow the player's Y-axis
        if (isAttachedToPlayer && playerTransform != null)
        {
            Vector3 cameraPosition = mainCamera.transform.position;
            cameraPosition.y = playerTransform.position.y;
            mainCamera.transform.position = cameraPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player collides with the tile, attach the camera to the player
        if (collision.CompareTag("Player"))
        {
            playerTransform = collision.transform;
            isAttachedToPlayer = true;
        }
    }
}
