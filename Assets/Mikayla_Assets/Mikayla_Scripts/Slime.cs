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

    void Start()
    {
        animator.SetFloat("Speed", 0);
        rb = GetComponent<Rigidbody2D>();
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
            Attack();  // Trigger attack when in range
        }
        else
        {
            rb.velocity = Vector2.zero;  // Stop movement when out of range
        }
        }
}
