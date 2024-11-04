using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyDecorator : Enemy
{
    protected Enemy enemy;

    // No constructor since we can't pass parameters to MonoBehaviour
    protected virtual void Awake()
    {
        // Find the Enemy component attached to the same GameObject
        enemy = GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError("EnemyDecorator requires an Enemy component on the same GameObject.");
        }
    }

    public override void TakeDamage(float damage)
    {
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    public override void Die()
    {
        if (enemy != null)
        {
            enemy.Die();
        }
    }

    public override void Attack()
    {
        if (enemy != null)
        {
            enemy.Attack();
        }
    }

    // Decorators can override other methods as needed
}
