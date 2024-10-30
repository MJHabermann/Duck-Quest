using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TouchMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 targetPosition;
    private bool isMoving;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
        rb.gravityScale = 0; // Ensure no unintended gravity
    }

    void Update()
    {
        // Check if there's a touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Process only if it's a touch begin phase
            if (touch.phase == TouchPhase.Began)
            {
                // Convert touch position to world position and set target
                Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touch.position);
                targetPosition = new Vector3(touchWorldPosition.x, touchWorldPosition.y, transform.position.z);
                isMoving = true;
            }
        }
    }

    void FixedUpdate()
    {
        // Only move if there's a significant distance to the target
        if (isMoving)
        {
            Vector2 direction = ((Vector2)targetPosition - rb.position).normalized;
            rb.velocity = direction * speed;

            // Stop moving if close enough to the target
            if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
            {
                rb.velocity = Vector2.zero;
                isMoving = false;
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // Ensure the player stops when not moving
        }
    }
}
