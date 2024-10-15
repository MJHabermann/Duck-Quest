using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float collisionOffset = 1f;
    public ContactFilter2D movementFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float moveSpeed = 2.5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject swordHitbox;
    Collider2D swordCollider;
    
    public AudioSource playerStep;
    
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        swordCollider=swordHitbox.GetComponent<Collider2D>();
    }

    void FixedUpdate(){
        //if there is movement...
        if(moveInput != Vector2.zero){
            //try to move player in input direction
            bool success = MovePlayer(moveInput);
            //if there was a collision, then...
            if(!success){
                //try left/right movement
                success = MovePlayer(new Vector2(moveInput.x, 0));
                //otherwise
                if(!success){
                    //try up/down movement
                    success = MovePlayer(new Vector2(0, moveInput.y));
                }
            }
            animator.SetBool("isMoving", success);
            //if player is moving and step sound is not
            if(success && !playerStep.isPlaying)
            {
                //then play step sound
                playerStep.Play();
            }
        }else{
            animator.SetBool("isMoving", false);
            //if player stops moving
            if(playerStep.isPlaying)
            {
                //stop sound
                playerStep.Stop();
            }
        }
    }

    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
        //If the player moves, set the animation to the correct direction
        if(moveInput != Vector2.zero){
            animator.SetFloat("XInput",moveInput.x);
            animator.SetFloat("YInput",moveInput.y);
            //change the direction of the sword hitbox to...
            if(moveInput.x > 0){        //right
                gameObject.BroadcastMessage("PlayerDirection", 2);
            }else if (moveInput.x < 0){ //left
                gameObject.BroadcastMessage("PlayerDirection", 4);
            }
            if(moveInput.y > 0){        //up
                gameObject.BroadcastMessage("PlayerDirection", 1);
            }else if (moveInput.y < 0){ //down
                gameObject.BroadcastMessage("PlayerDirection", 3);
            }
        }
    }

    //Check if player will collide with other objects and return result.
    public bool MovePlayer(Vector2 direction){
        //Check if there are any collisions in intended directions
        int count = rb.Cast(direction,
                            movementFilter,
                            castCollisions,
                            moveSpeed * Time.fixedDeltaTime + collisionOffset);
        //if none, update position and return true
        if(count == 0){
            Vector2 moveVector = direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveVector);
            return true;
        }else {
            return false;
        }
    }

    void OnFire(){
        gameObject.BroadcastMessage("swordAttack", true);
        animator.SetTrigger("swordAttack");
    }

}
