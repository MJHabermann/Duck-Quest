using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyDecorator : Enemy
{
    protected Enemy enemy;

    public EnemyDecorator(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public override void TakeDamage(float damage)
    {
        enemy.TakeDamage(damage);
    }

    public override void Die()
    {
        enemy.Die();
    }

    public override void Attack()
    {
        enemy.Attack();
    }

    // Decorators can override other methods as needed
}

