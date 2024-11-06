using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackDecorator : EnemyDecorator
{
    public GameObject projectilePrefab;  // Prefab for the projectile
    public Transform firePoint;          // The point from which the projectile will be fired
    public float projectileSpeed = 10f;  // Speed of the projectile
    public float projectileDamage = 1f;  // Damage dealt by the projectile
    public float cooldownTime = 5f;      // Cooldown time between ranged attacks
    private float lastAttackTime;
    public Transform target;             // Target of the ranged attack

    private Rigidbody2D bossRb;

    public void SetBossRigidbody(Rigidbody2D rb)
    {
        bossRb = rb;
    }

    public override void Attack()
    {
        if (Time.time - lastAttackTime >= cooldownTime)
        {
            animator.SetBool("IsAttacking", true);
            lastAttackTime = Time.time;

            // Perform the ranged attack
            FireProjectile();
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    private void FireProjectile()
    {
        if (firePoint != null && projectilePrefab != null && target != null)
        {
            Debug.Log($"{EnemyName} fired a ranged attack!");

            // Instantiate the projectile at the fire point
            GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Configure the projectile's speed, damage, and direction
            Projectile projectile = projectileInstance.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.speed = projectileSpeed;
                projectile.damage = projectileDamage;

                // Calculate the direction to the target
                Vector2 direction = (target.position - firePoint.position).normalized;

                // Set projectile velocity in the direction of the target
                Rigidbody2D projectileRb = projectileInstance.GetComponent<Rigidbody2D>();
                if (projectileRb != null)
                {
                    projectileRb.velocity = direction * projectileSpeed;

                    // Optionally, rotate the projectile to face the direction
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    projectileInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                }
                else
                {
                    Debug.LogError("Projectile Rigidbody2D is null!");
                }
            }
            else
            {
                Debug.LogError("Projectile script is missing from the projectile prefab!");
            }
        }
        else
        {
            Debug.LogWarning("Projectile prefab, fire point, or target is missing!");
        }
    }
}
