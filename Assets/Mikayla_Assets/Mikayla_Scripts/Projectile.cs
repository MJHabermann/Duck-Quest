using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // Reference to the projectile prefab
    public float speed = 10f;
    public float damage = 1f;
    public float lifetime = 2f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Projectile instantiated at position: " + transform.position);
        Destroy(gameObject, lifetime); // Set a timer to destroy the projectile after its lifetime
    }

    private void Update()
    {
        // Move the projectile forward based on its speed
        transform.Translate(Vector3.right * speed * Time.deltaTime);
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
        else
        {
            Destroy(gameObject); // Destroy if it hits something else
        }
    }

//     void OnTriggerEnter(Collider collision)
//     {
//     // Check if the object hit has the tag "Player"
//     if (collision.CompareTag("Player"))
//     {
//         Debug.Log("projectile collided with the player!");
//         // Get the IDamageable component from the player (optional)
//         IDamageable damageable = collision.GetComponent<IDamageable>();

//         if (damageable != null)
//         {
//             damageable.OnHit(damage);  // Apply damage to the player
//             Debug.Log("Player hit by projectile!");
//         }

//         // Destroy the projectile after it hits the player
//         Destroy(gameObject);
//     }
//     else // if projectile hits environment
//     {
//         // Optional: Handle case where projectile hits the environment and should be destroyed
//         Destroy(gameObject);
//     }
// }

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

