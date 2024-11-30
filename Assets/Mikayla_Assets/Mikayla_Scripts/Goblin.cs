using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    public Transform[] waypoints;  // Points the Goblin will patrol betweens
    public float detectionRange = 5f; // How close the player needs to be for the Goblin to detect them
    public float chaseSpeed = 2f; // Speed when chasing the player
    public float attackRange = 1f;
    public float attackCooldown = 2f; 
    private float lastAttackTime;

    private int currentWaypointIndex = 0;
    private bool isChasing = false;
    private Vector2 targetPosition;
    public float knockbackDuration = 0.2f; 
    // Start is called before the first frame update
    void Start()
    {
        //Set attributes
        Health = 5f;               // Set goblin's health
        EnemyName = "Goblin";        // Set the name of the enemy
        Speed = 1.5f;                 // Set goblin's movement speed
        Damage = 1f;

        target = GameObject.FindWithTag("Player").transform;
        playerRb = target.GetComponent<Rigidbody2D>(); 
        rb = GetComponent<Rigidbody2D>();
        targetPosition = waypoints[currentWaypointIndex].position;
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on the Goblin prefab!");
        }
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
        MoveTowards(targetPosition, Speed);
    }

    void ChasePlayer()
    {
        targetPosition = target.position;
        MoveTowards(targetPosition, chaseSpeed);
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

    public void SetWaypoints(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
    }
}

