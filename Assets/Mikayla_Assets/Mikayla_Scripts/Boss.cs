using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public GameObject projectilePrefab;  // Prefab for the projectile
    public Transform firePoint;          // Point from which projectiles will be fired
    public float bossAttackCooldown = 1.5f; // Cooldown between attacks
    public float chaseSpeed = 4f;        // Boss chase speed
    public float bossAttackRange = 15f;  // Range for ranged attack

    private bool isChasing = false;
    private Transform target;

    void Start()
    {
        // Find the player as the target
        target = GameObject.FindWithTag("Player").transform;

        // Apply the ranged attack behavior dynamically using the decorator
        ChangeBehavior();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer <= bossAttackRange)
        {
            // Boss within attack range, attempt to attack
            Attack();
        }
        else
        {
            // Start chasing the player
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        // Chase the player towards the target position
        MoveTowards(target.position, chaseSpeed);
    }

    public override void Attack()
    {
        // Use the decorated ranged attack behavior
        base.Attack();
        Debug.Log("Boss is attacking with a ranged attack!");
    }

    void ChangeBehavior()
    {
        // Add a ranged attack behavior to the Boss dynamically using the RangedAttackDecorator
        Enemy bossWithRangedAttack = new RangedAttackDecorator(this, projectilePrefab, firePoint);

    }
}

