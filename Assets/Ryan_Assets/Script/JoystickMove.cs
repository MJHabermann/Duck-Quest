using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    public GameObject joystickGameObject; // Reference to the joystick GameObject
#if UNITY_ANDROID
    public Joystick movementJoystick; // Joystick reference, only for Android
#endif

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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        reachCollider = reach.GetComponent<Collider2D>();
        playerDirection = new Vector3(0, 0, 0);

#if UNITY_ANDROID
        // Activate joystick only on Android
        if (joystickGameObject != null)
        {
            joystickGameObject.SetActive(true);
        }
#else
        // Deactivate joystick on non-Android platforms
        if (joystickGameObject != null)
        {
            joystickGameObject.SetActive(false);
        }
#endif
    }

    private void FixedUpdate()
    {
#if UNITY_ANDROID
        // Get input direction from joystick on Android
        moveInput = movementJoystick.Direction;
#endif

        // If there is movement input...
        if (moveInput != Vector2.zero)
        {
            bool success = MovePlayer(moveInput);

            // If collision occurs, try alternate directions
            if (!success)
            {
                success = MovePlayer(new Vector2(moveInput.x, 0)) || MovePlayer(new Vector2(0, moveInput.y));
            }

            animator.SetBool("isMoving", success);
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
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isMoving", false);
        }
    }

    public bool MovePlayer(Vector2 direction)
    {
        int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            Vector2 moveVector = direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveVector);
            return true;
        }
        else
        {
            return false;
        }
    }
}
