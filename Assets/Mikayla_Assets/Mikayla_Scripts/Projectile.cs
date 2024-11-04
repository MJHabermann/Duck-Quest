using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 3f;
    public float damage = 1f;       // Amount of damage the projectile deals
    public float lifetime = 2f;     // Lifetime before it despawns
    public Transform target; // Reference to the player

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        //Rigidbody2D playerRb = target.GetComponent<Rigidbody2D>(); 
        Debug.Log("Projectile instantiated at position: " + transform.position);
    }

    void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision with player or environment
        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Debug.Log("Player hit by ranged attack");
            damageable.OnHit(damage);
        }
        else{
            Debug.Log("object is null");
        }

        // Destroy the projectile on impact
        Destroy(gameObject); // Destroy projectile on impact
    }

}

