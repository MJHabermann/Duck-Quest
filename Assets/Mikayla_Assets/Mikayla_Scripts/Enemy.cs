using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;       // Use private access with SerializeField to keep it visible in the Inspector
    [SerializeField] private string enemyName;
    [SerializeField] private float damage;
    protected Rigidbody2D playerRb;
    public float knockbackForce = 5f;   
     public Transform target;

    private bool isFacingRight = false;          // Keep track of current facing direction

    public Animator animator;

    [SerializeField] private float speed;
    protected Rigidbody2D rb;

    public float Health
    {
        get { return health; }
        set
        {
            health = Mathf.Max(0, value);  // Ensure health doesn't go below 0
            if (health == 0)
            {
                Die();
            }
        }
    }

    public string EnemyName
    {
        get { return enemyName; }
        set { enemyName = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = Mathf.Max(0, value); }   // Ensure speed is non-negative
    }

        public float Damage
    {
        get { return damage; }
        set { speed = Mathf.Max(0, value); }   // Ensure damage is non-negative
    }

    public bool IsFacingRight
    {
        get { return isFacingRight; }
        private set { isFacingRight = value; } // Facing direction is set internally
    }

    public T Spawn<T>(GameObject enemyPrefab, Vector3 position, Quaternion rotation) where T : Enemy
    {
        // Instantiate the enemy prefab
        GameObject enemyInstance = Instantiate(enemyPrefab, position, rotation);

        // Return the specific enemy type (e.g., Slime, or any subclass of Enemy)
        return enemyInstance.GetComponent<T>();
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;  // Use the Health property to adjust health
        Debug.Log("Enemy hit");
    }

    public virtual void Die()
    {
        animator.SetBool("isDead", true);
        float deathAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, deathAnimationLength + .2f);
    }

    public virtual void Flip(Vector3 direction)
    {
        if (direction.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
        else if (direction.x < 0 && isFacingRight)
        {
            isFacingRight = false;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
    void ApplyKnockback()
    {
        // Calculate the direction from the enemy to the player
        Vector2 knockbackDirection = (target.position - transform.position).normalized;
        // Apply force to the player's Rigidbody2D in the opposite direction
        playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

    }

    //public void Attack()
    public virtual void Attack()
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

    public virtual void MoveTowards(Vector2 target, float speed)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        Flip(direction);
        rb.velocity = direction * speed;
    }
}