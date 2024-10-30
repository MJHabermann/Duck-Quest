using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    public Vector3 offset;
    private float smoothSpeed = 0.125f;
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
        else
        {
            // Try finding the player again if it was lost or destroyed
            FindPlayer();
        }
    }
    void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("No GameObject with tag 'Player' found in the scene.");
        }
    }
}
