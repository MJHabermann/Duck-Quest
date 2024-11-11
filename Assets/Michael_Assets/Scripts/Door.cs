using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //These variables define characteristics about which direction the door is facing and how far the camera is going
    public enum PanDirection { Up, Down, Left, Right }
    public PanDirection direction;
    public float panSpeed = 2.0f;
    public float panDistance = 13.0f; 
    public float playerDistance = 8.0f;
    //These are the sprites that will be used for both types of doors
    public Sprite closedDoorSprite;
    public Sprite openDoorSprite;
    
    //These are variables that are shared with the class and its children but I do not want the Inspector to have direct access to
    //The reason for protected variables on these is because I wanted the security of private variables with the children inheriting them for possible future use
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
    protected bool isOpened = true;
    //This function initilizes everything
    public virtual void Start()
    {
        doorSpriteRenderer = GetComponent<SpriteRenderer>(); //Gets the spriterenderer of the object
        mainCamera = Camera.main; //Gets main camera
        startPosition = mainCamera.transform.position; //Gets the camera position for the offset
        player = GameObject.FindGameObjectWithTag("Player"); //Finds the player
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>(); //Gets the player sprite renderer for disabling the player sprite as you enter and exit doors
        UpdateDoorSprite();
    }
    //This is our update function that always checks if the door is panning
    public virtual void Update()
    {
        //If the camera is panning because the player collided with the door
        if (isPanning)
        {
            //Start moving the camera and player position
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, panSpeed * Time.deltaTime);
            player.transform.position = Vector3.Lerp(player.transform.position, playerTargetPosition, panSpeed * Time.deltaTime);
            //This is what happens when the camera has moved up enough, signfying we have stopped moving
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
    //if the player has hit the door start the panning
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransitioning && isOpened)
        {
            StartPanning();
        }
    }
    //We will start panning if this function is called
    public virtual void StartPanning()
    {
        //We want to make sure that we are not panning or transitioning because at some point the above function will be touching the door at least twice between transitions
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
    //Decides the direction based on the door
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
    //Decides the direction based on the door
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
    //Updates the door sprite based on position or whether it is open or not
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
        //This rotates the direction of the door based on the direction specified by the user
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


}

