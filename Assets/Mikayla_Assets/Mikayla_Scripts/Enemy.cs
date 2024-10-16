using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] string enemyName;
    [SerializeField] int baseAttack;

    private bool isFacingRight = false; // Keep track of current facing direction

    public Animator animator;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        //Destroy(gameObject);
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
}
