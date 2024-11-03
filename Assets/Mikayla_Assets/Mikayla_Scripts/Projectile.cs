using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 1f;       // Amount of damage the projectile deals
    public float lifetime = 2f;     // Lifetime before it despawns

    void Start()
    {
        // Destroy the projectile after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Handle collision with player or environment
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.OnHit(damage);
            Destroy(gameObject);  // Destroy projectile on impact
        }
    }
}

