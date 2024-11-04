using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackDecorator : EnemyDecorator
{
    public GameObject projectilePrefab;  // Prefab for the projectile
    public Transform firePoint;          // The point from which the projectile will be fired
    public float projectileSpeed = 10f;  // Speed of the projectile
    public float cooldownTime = 5f;      // Cooldown time between ranged attacks
    private float lastAttackTime;
    public Transform target;


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
            animator.SetBool("IsAttacking", false); // Ensure animator is not null
        }
    }

    private void FireProjectile()
    {
        if (firePoint != null && projectilePrefab != null && target != null)
        {
            Debug.Log($"{enemyName} fired a ranged attack!");

            // Instantiate the projectile at the fire point with the correct rotation
            GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Get the Rigidbody2D component of the instantiated projectile
            Rigidbody2D projectileRb = projectileInstance.GetComponent<Rigidbody2D>();
            if (projectileRb != null)
            {
                // Calculate the direction to the target and set the projectile's velocity
                Vector2 direction = (target.position - firePoint.position).normalized;
                projectileRb.velocity = direction * projectileSpeed;

                // Optionally, set the projectile's rotation to face the target direction
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
            Debug.LogWarning("Projectile prefab, fire point, or target is missing!");
        }
    }

}
