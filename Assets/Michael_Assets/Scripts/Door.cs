using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum PanDirection { Up, Down, Left, Right }
    public PanDirection direction;
    public float panSpeed = 2.0f;
    public float panDistance = 13.0f;
    
    public Sprite closedDoorSprite;
    public Sprite openDoorSprite;
    public float playerDistance = 8.0f;

    protected SpriteRenderer doorSpriteRenderer;
    protected Camera mainCamera;
    protected Vector3 startPosition;
    protected bool isPanning = false;
    protected Vector3 targetPosition;
    protected GameObject player;
    protected Vector3 playerTargetPosition;
    protected static bool isTransitioning = false;
    protected SpriteRenderer playerSpriteRenderer;
    protected Vector3 offset = Vector3.zero;
    protected static bool isOpened = true;
    protected static List<NormalDoor> allNormalDoors = new List<NormalDoor>();
    public virtual void Start()
    {
        doorSpriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        startPosition = mainCamera.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        UpdateDoorSprite();
    }

    public virtual void Update()
    {
        if (isPanning)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, panSpeed * Time.deltaTime);
            player.transform.position = Vector3.Lerp(player.transform.position, playerTargetPosition, panSpeed * Time.deltaTime);

            if (Vector3.Distance(mainCamera.transform.position, targetPosition) < 0.01f && Vector3.Distance(player.transform.position, playerTargetPosition) < 0.01f)
            {
                mainCamera.transform.position = targetPosition;
                player.transform.position = playerTargetPosition;
                isPanning = false;
                isTransitioning = false;
                playerSpriteRenderer.enabled = true;
            }
        }
    }

    public virtual void StartPanning()
    {
        if (!isPanning && !isTransitioning)
        {
            isPanning = true;
            isTransitioning = true;
            startPosition = mainCamera.transform.position;
            targetPosition = GetTargetPosition();
            playerTargetPosition = GetPlayerTargetPosition();
            playerSpriteRenderer.enabled = false;
        }
    }

    protected Vector3 GetTargetPosition()
    {
        switch (direction)
        {
            case PanDirection.Up:
                offset = new Vector3(0, panDistance, 0);
                break;
            case PanDirection.Down:
                offset = new Vector3(0, -panDistance, 0);
                break;
            case PanDirection.Left:
                offset = new Vector3(-panDistance, 0, 0);
                break;
            case PanDirection.Right:
                offset = new Vector3(panDistance, 0, 0);
                break;
        }

        return startPosition + offset;
    }

    protected Vector3 GetPlayerTargetPosition()
    {
        switch (direction)
        {
            case PanDirection.Up:
                offset = new Vector3(0, playerDistance, 0);
                break;
            case PanDirection.Down:
                offset = new Vector3(0, -playerDistance, 0);
                break;
            case PanDirection.Left:
                offset = new Vector3(-playerDistance, 0, 0);
                break;
            case PanDirection.Right:
                offset = new Vector3(playerDistance, 0, 0);
                break;
        }

        return player.transform.position + offset;
    }

    protected virtual void UpdateDoorSprite()
    {
        if (isOpened)
        {
            doorSpriteRenderer.sprite = openDoorSprite;
            doorSpriteRenderer.gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        else
        {
            doorSpriteRenderer.sprite = closedDoorSprite;
            doorSpriteRenderer.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }

        // Rotate door to match direction
        switch (direction)
        {
            case PanDirection.Up:
                doorSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case PanDirection.Down:
                doorSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case PanDirection.Left:
                doorSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case PanDirection.Right:
                doorSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransitioning && isOpened)
        {
            StartPanning();
        }
    }
}

