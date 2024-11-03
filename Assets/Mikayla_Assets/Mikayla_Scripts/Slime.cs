using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Slime : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public float attackDamage = 1f;
    private Rigidbody2D playerRb;
    public float knockbackForce = 5f;   
    public float attackCooldown = 2f; 
    private float lastAttackTime;

    void Start()
    {
        animator.SetFloat("Speed", 0);
        rb = GetComponent<Rigidbody2D>();
        playerRb = target.GetComponent<Rigidbody2D>(); 
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    { 
        CheckDistance();
        float currentSpeed = rb.velocity.magnitude;
        animator.SetFloat("Speed", currentSpeed);
    }
    void CheckDistance(){
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius){
            Vector3 direction = (target.position - transform.position).normalized;
            //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            rb.velocity = direction * speed; 
            Flip(direction);
        }
        else if (distance <= attackRadius)
        {
            rb.velocity = Vector2.zero;  // Stop the enemy from moving
            TryAttackPlayer();  // Trigger attack when in range
        }
        else
        {
            rb.velocity = Vector2.zero;  // Stop movement when out of range
        }
        }
    void ApplyKnockback()
    {
        // Calculate the direction from the enemy to the player
        Vector2 knockbackDirection = (target.position - transform.position).normalized;
        // Apply force to the player's Rigidbody2D in the opposite direction
        playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

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
            animator.SetBool("IsAttacking", true);
            // Inflict damage
            damageableObject.OnHit(attackDamage);
            Debug.Log("Enemy attacked the player!");
            ApplyKnockback();
        }
        else
        {
            Debug.LogWarning("Player does not implement IDamageable!");
        }
    }
}
