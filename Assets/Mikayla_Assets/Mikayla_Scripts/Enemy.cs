using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;       // Use private access with SerializeField to keep it visible in the Inspector
    [SerializeField] private string enemyName;

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

    public virtual void Attack()
    {
        animator.SetBool("IsAttacking", true);
    }

    public virtual void MoveTowards(Vector2 target, float speed)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        Flip(direction);
        rb.velocity = direction * speed;
    }
}