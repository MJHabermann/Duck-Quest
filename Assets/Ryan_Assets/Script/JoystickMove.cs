using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoystickMove : MonoBehaviour
{

    public Joystick movementJoystick;
    public float moveSpeed = 4f;
    private Rigidbody2D rb;

    public float collisionOffset = 1f;
    public ContactFilter2D movementFilter;
    public GameObject reach;
    public Collider2D reachCollider;
    public Quaternion rotation;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Vector2 moveInput;
    private Animator animator;
    private Vector3 playerDirection;

    //public AudioSource playerStep;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        reachCollider = reach.GetComponent<Collider2D>();
        playerDirection = new Vector3(0, 0, 0);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
    // Get input direction directly from the joystick
    moveInput = movementJoystick.Direction;

    // If there is movement input...
    if (moveInput != Vector2.zero)
    {
        // Try to move player in input direction
        bool success = MovePlayer(moveInput);

        // If collision occurs, try alternate directions
        if (!success)
        {
            success = MovePlayer(new Vector2(moveInput.x, 0)) || MovePlayer(new Vector2(0, moveInput.y));
        }

        // Set isMoving based on movement success
        animator.SetBool("isMoving", success);

        // Update animator direction parameters
        animator.SetFloat("XInput", moveInput.x);
        animator.SetFloat("YInput", moveInput.y);

        // Adjust player direction based on input for animation purposes
        if (moveInput.x > 0)
            playerDirection = new Vector3(0, 0, 0); // Right
        else if (moveInput.x < 0)
            playerDirection = new Vector3(0, 0, 180); // Left
        if (moveInput.y > 0)
            playerDirection = new Vector3(0, 0, 90); // Up
        else if (moveInput.y < 0)
            playerDirection = new Vector3(0, 0, 270); // Down

        // Optionally, broadcast player direction changes if other scripts need it
        //gameObject.BroadcastMessage("PlayerDirection", playerDirection, SendMessageOptions.DontRequireReceiver);
    }
    else
    {
        // No input detected, stop movement
        rb.velocity = Vector2.zero;
        animator.SetBool("isMoving", false);
    }
}

    public bool MovePlayer(Vector2 direction){
        //Check if there are any collisions in intended directions
        int count = rb.Cast(direction,
                            movementFilter,
                            castCollisions,
                            moveSpeed * Time.fixedDeltaTime + collisionOffset);
        //if none, update position and return true
        if(count == 0){
            Vector2 moveVector = direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveVector);
            return true;
        }else {
            return false;
        }
    }
}