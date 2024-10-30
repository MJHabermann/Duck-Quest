using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackDecorator : EnemyDecorator
{
    public GameObject projectilePrefab;  // Prefab for the projectile
    public Transform firePoint;          // The point from which the projectile will be fired
    public float projectileSpeed = 10f;  // Speed of the projectile
    public float attackRange = 10f;      // Range within which the enemy can perform a ranged attack
    public float cooldownTime = 2f;      // Cooldown time between ranged attacks
    private float lastAttackTime;
    public Transform target;

    public RangedAttackDecorator(Enemy enemy, GameObject projectilePrefab, Transform firePoint) : base(enemy)
    {
        this.projectilePrefab = projectilePrefab;
        this.firePoint = firePoint;
    }

    public override void Attack()
    {
        if (Time.time - lastAttackTime >= cooldownTime)
        {
            lastAttackTime = Time.time;  // Reset the attack cooldown
            base.Attack();  // Call the base attack (can be melee or another decorator)

            // Perform the ranged attack
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        Debug.Log($"{enemyName} fired a ranged attack!");

        // Instantiate the projectile at the fire point
        GameObject projectileInstance = GameObject.Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Set the velocity of the projectile to move towards the player
        Rigidbody2D rb = projectileInstance.GetComponent<Rigidbody2D>();
        Vector2 direction = (target.position - firePoint.position).normalized;  // Direction towards the target
        rb.velocity = direction * projectileSpeed;
    }
}

