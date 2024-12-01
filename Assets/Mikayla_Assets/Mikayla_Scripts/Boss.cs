using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public GameObject projectilePrefab;
    public float chaseRadius = 10f;
    public Transform firePoint;
    public float bossAttackCooldown = 5f;
    public float chaseSpeed = 4f;
    public float bossAttackRange = 9f;
    bool facingRight = true;
    private RangedAttackDecorator rangedAttack; // Reference to the decorator
    private Boss boss;

    void Start()
    {
        //Set attributes
        Health = 20f;               // Set goblin's health
        EnemyName = "Boss";        // Set the name of the enemy
        Speed = 2f;                 // Set goblin's movement speed
        Damage = 1.5f;

        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        playerRb = target.GetComponent<Rigidbody2D>(); 

        // Check if target is assigned
        if (target == null)
        {
            Debug.LogError("Player target not found!");
            return;
        }

        // Dynamically add ranged attack behavior
        ChangeBehavior();
    }

    void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Flip(direction);
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        if (distanceToPlayer <= bossAttackRange)
        {
            Attack();
        }
        else
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        if (target != null)
        {
            Vector2 targetPosition = target.position;
            MoveTowards(targetPosition, chaseSpeed);
        }
    }

    public override void Attack()
    {
        if (rangedAttack != null)
        {
            // Use the decorated ranged attack behavior
            rangedAttack.Attack();
        }
        else
        {
            Debug.LogWarning("Ranged attack behavior not initialized.");
        }
    }

    void ChangeBehavior()
    {
        if (rangedAttack == null)
        {
            // Add the RangedAttackDecorator component and assign necessary fields
            rangedAttack = gameObject.AddComponent<RangedAttackDecorator>();
            rangedAttack.projectilePrefab = projectilePrefab;
            rangedAttack.firePoint = firePoint;
            rangedAttack.target = target;
            rangedAttack.SetBossRigidbody(rb);
            rangedAttack.animator = animator;
        }
    }

    public override void MoveTowards(Vector2 targetPosition, float speed)
    {
        float minDistance = 6.0f; // Minimum distance to maintain from the player
        float distanceToPlayer = Vector2.Distance(transform.position, targetPosition);

        if (distanceToPlayer > minDistance) { 
            // Move towards the player if further than the minimum distance
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized; 
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector2.zero; // Ensure no movement if the target is null
        }
    }
    public override void Flip(Vector3 direction) { 
        if (direction.x > 0 && !facingRight) { 
            facingRight = true; 
            Vector3 localScale = transform.localScale; 
            localScale.x *= -1; transform.localScale = localScale; 
        } 
        else if (direction.x < 0 && facingRight) 
        { 
            facingRight = false;
            Vector3 localScale = transform.localScale; 
            localScale.x *= -1; 
            transform.localScale = localScale; 
        } 
    }
}

// Choose a dynamically bound method. What method gets called now? 
// (In Start function)
// Enemy enemy = new Boss(); // Upcasting Slime to Enemy 
// enemy.Attack(); // Dynamic binding: calls Boss's Attack method at runtime 

// Pick a statically bound method. Which one would be called in each of the two previous cases?
// Enemy enemy = new Enemy(); enemy.Attack(); // Static binding: calls Enemy's Attack method 
//Boss boss = new Boss(); boss.Attack(); // Static binding: calls Boss's overridden Attack method

