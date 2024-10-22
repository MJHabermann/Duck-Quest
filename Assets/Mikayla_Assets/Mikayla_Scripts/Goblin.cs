using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    public Transform[] waypoints;  // Points the Goblin will patrol betweens
    public float detectionRange = 5f; // How close the player needs to be for the Goblin to detect them
    public Transform target; // Reference to the player
    public float chaseSpeed = 3.5f; // Speed when chasing the player
    public float attackRange = 1f;
    public float attackCooldown = 2f; 
    private float lastAttackTime;

    private int currentWaypointIndex = 0;
    private bool isChasing = false;
    private Rigidbody2D rb;
    private Vector2 targetPosition;
    public float attackDamage = 1f;
    private Rigidbody2D playerRb;
    public float knockbackForce = 5f;    // Force applied to the player during knockback
    public float knockbackDuration = 0.2f; 
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        playerRb = target.GetComponent<Rigidbody2D>(); 
        rb = GetComponent<Rigidbody2D>();
        targetPosition = waypoints[currentWaypointIndex].position;
    }


    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer <= attackRange)
        {
            // Player is within attack range, attempt to attack
            TryAttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // Player detected, start chasing
            isChasing = true;
        }
        else
        {
            // Continue patrolling
            isChasing = false;
        }

        if (isChasing && distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }
        else if (!isChasing)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        targetPosition = waypoints[currentWaypointIndex].position;
        MoveTowards(targetPosition, speed);
    }

    void ChasePlayer()
    {
        targetPosition = target.position;
        MoveTowards(targetPosition, chaseSpeed);
    }

    void MoveTowards(Vector2 target, float speed)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        Flip(direction);
        rb.velocity = direction * speed;
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the detection range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    void TryAttackPlayer()
    {
        // If it's time to attack again
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;  // Reset the attack cooldown
            Attack();
        }
    }

    public override void Attack()
    {
        // Check if the player implements IDamageable
        IDamageable damageableObject = target.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            animator.SetBool("isAttacking", true);
            // Inflict damage
            damageableObject.OnHit(attackDamage);
            Debug.Log("Goblin attacked the player!");
            ApplyKnockback();
        }
        else
        {
            Debug.LogWarning("Player does not implement IDamageable!");
        }
    }
    void ApplyKnockback()
    {
        // Calculate the direction from the enemy to the player
        Vector2 knockbackDirection = (target.position - transform.position).normalized;
        // Apply force to the player's Rigidbody2D in the opposite direction
        playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(StopPlayerMovementTemporarily());

    }
    IEnumerator StopPlayerMovementTemporarily()
    {
        // Save the current velocity of the player
        Vector2 originalVelocity = playerRb.velocity;

        // Temporarily disable movement
        playerRb.velocity = Vector2.zero;

        // Wait for the knockback duration
        yield return new WaitForSeconds(knockbackDuration);

        // Restore the player's velocity (or you could allow the player to regain control here)
        playerRb.velocity = originalVelocity;
    }
}
