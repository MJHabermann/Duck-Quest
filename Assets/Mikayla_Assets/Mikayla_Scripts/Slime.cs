using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Slime : Enemy
{
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public float attackCooldown = 2f; 
    private float lastAttackTime;

    void Start()
    {
        //Set attributes
        Health = 3f;               // Set slime's health
        EnemyName = "Slime";        // Set the name of the enemy
        Speed = 1.5f;                 // Set slime's movement speed
        Damage = 1f;

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
            rb.velocity = direction * Speed; 
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
            damageableObject.OnHit(Damage);
            Debug.Log("Enemy attacked the player!");
            ApplyKnockback();
        }
        else
        {
            Debug.LogWarning("Player does not implement IDamageable!");
        }
    }
}

// Copyright argument
// The four factors judges consider in fair use are:
// Purpose and character of your use (nonprofit/educational vs. commercial) (transformative)
// Nature of the copyrighted work (factual vs. creative)
// Amount and substantiality of the portion taken.
// Effect of the use upon the potential market

// Transformative: In the video game, the enemy slime sprite is integrated into a larger 
// interactive experience that involves unique gameplay, story, sound, and additional visual assets.
// This integration changes the context of the original slime sprite, making it a component 
//of a broader work rather than a standalone asset. Transforming the sprite’s function within 
//the game and placing it in a new, creative environment aligns with fair use principles.

// Different Market and Purpose: When a purchased sprite is modified and integrated into a 
//larger game, the market for the game is generally distinct from the market for the original 
//sprite. The sprite, as sold, is intended for other developers to use in their own projects,
// not for end users to experience directly. By contrast, the market for the game appeals to 
//players seeking interactive entertainment, rather than developers seeking reusable assets. 
// This difference in audience and purpose helps support a fair use argument because the 
// commercial game does not directly compete with or replace the need for the original sprite asset.

// No Harm to Original Sprite Sales: If the original sprite is modified and only used within 
//the context of a larger game, this generally doesn’t reduce the demand for the original sprite. 
//Other developers can still purchase and use the original asset in their own projects, 
//meaning the market for the sprite itself remains intact. As long as the game doesn’t sell the 
//sprite in isolation or offer it in a way that competes with the original, there is minimal 
//impact on the market for the asset.