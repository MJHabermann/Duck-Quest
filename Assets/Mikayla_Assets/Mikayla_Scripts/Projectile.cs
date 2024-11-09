using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // Reference to the projectile prefab
    public float speed = 3f;
    public float damage = 1f;
    public float lifetime = 5f;

    private Rigidbody2D rb;
    private Collider2D col;
   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>(); // Get the Collider2D component
        Debug.Log("Projectile instantiated at position: " + transform.position);
        StartCoroutine(DisableTriggerAfterOneFrame());
        Destroy(gameObject, lifetime); // Set a timer to destroy the projectile after its lifetime
    }

     private void Update()
    {
        // Move the projectile forward based on its speed
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    
    }

    private IEnumerator DisableTriggerAfterOneFrame()
    {
        // Wait for one frame
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;

        // Disable the trigger mode after one frame
        if (col != null)
        {
            col.isTrigger = false; // Disable the trigger mode, enabling normal collision
            Debug.Log("Trigger mode disabled after one frame.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object hit has the tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("projectile collided with the player!");
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.OnHit(damage);
                Debug.Log("Player hit by projectile!");
            }
            Destroy(gameObject); // Destroy the projectile after hitting the player
        }
        // else
        // {
        //     Destroy(gameObject); // Destroy if it hits something else
        // }
    }

    // Method to spawn the projectile
    public static void SpawnProjectile(GameObject projectilePrefab, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (projectilePrefab != null)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, spawnPosition, spawnRotation);
            Debug.Log("Projectile spawned at position: " + spawnPosition);
        }
        else
        {
            Debug.LogWarning("Projectile prefab is missing!");
        }
    }
}

