using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] public string enemyName;

    protected bool isFacingRight = false; // Keep track of current facing direction

    public Animator animator;

    public float speed;
    protected Rigidbody2D rb;

    public T Spawn<T>(GameObject enemyPrefab, Vector3 position, Quaternion rotation) where T : Enemy
    {
        // Instantiate the enemy prefab
        GameObject enemyInstance = Instantiate(enemyPrefab, position, rotation);

        // Return the specific enemy type (e.g., Slime, or any subclass of Enemy)
        return enemyInstance.GetComponent<T>();
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Enemy hit");
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        animator.SetBool("isDead", true);
        float deathAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, deathAnimationLength + .2f);
    }

    public virtual void Flip(Vector3 direction){
        if (direction.x > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            
        }
        else if (direction.x < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            
        }
    }
    public virtual void Attack(){
        animator.SetBool("IsAttacking", true);
    }
    public virtual void MoveTowards(Vector2 target, float speed)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        Flip(direction);
        rb.velocity = direction * speed;
    }
}
