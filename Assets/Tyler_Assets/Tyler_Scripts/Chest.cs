using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private bool closed;
    void Start()
    {
        closed = true;
        animator = GetComponent<Animator>();
        animator.SetBool("isClosed", true);
        rb = GetComponent<Rigidbody2D>();
    }

    public void Open(){
        if(closed){
            closed = false;
            animator.SetBool("isClosed", false);
        }
    }
}
