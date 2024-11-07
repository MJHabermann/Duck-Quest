
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float collisionOffset = 1f;
    public ContactFilter2D movementFilter;
    public float moveSpeed = 4f;
    public GameObject reach;
    public GameObject bomb;
    public GameObject arrow;
    public GameObject hook;
    public GameObject magic;
    public Collider2D reachCollider;
    public AudioSource playerStep;
    public Quaternion rotation;
    // public float actionCooldown = .5f;
    // private float actionCooldownStart = 0f;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;
    private Sword sword = new Sword();
    private int bombCount;
    private int arrowCount;
    private Vector3 playerDirection;
    private bool isDead = false;
    private bool isOccupied = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        reachCollider = reach.GetComponent<Collider2D>();
        bombCount = 10;
        arrowCount = 10;
        playerDirection = new Vector3(0, 0, 0);
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

    void OnBomb(){
        if(!isDead && !isOccupied){
            //check if there is already a bomb in play
            if(GameObject.Find("Bomb(Clone)") == null){
                //create bomb if available
                if(bombCount > 0){
                Instantiate(bomb, transform.position, transform.rotation);
                bombCount--;
                Debug.Log("Bombs left: " + bombCount);
            }else{
                Debug.Log("No bombs");
            }
            }else{
                Debug.Log("Please wait, there is already a bomb!");
            }
        }
        
    }

    void OnBow(){
        if(!isDead && !isOccupied){
            if(arrowCount > 0){
                //set player direction
                rotation = Quaternion.Euler(playerDirection);

                //spawn arrow, at player location, in same direction as player
                Instantiate(arrow, transform.position, rotation);
                arrowCount--;
                Debug.Log("Arrows left: " + arrowCount);
            }else{
                Debug.Log("No arrows");
            }
        }
    }

    void OnHook(){
        if(!isDead && !isOccupied){
            if(hook != null){
                //set player direction
                rotation = Quaternion.Euler(playerDirection);

                //spawn hook, at player location, in same direction as player
                isOccupied = true;
                moveInput = Vector2.zero;
                Instantiate(hook, transform.position, rotation);
            }else{
                Debug.Log("No hook");
            }
        }
    }

    void OnInteract(){
        if(!isDead && !isOccupied){
            //tell the ReachHitbox to interact
            gameObject.BroadcastMessage("Interact");

            animator.SetTrigger("Interact");
        }
    }

    void OnJump(){
        if(!isDead && !isOccupied){
            Debug.Log("switched to magic sword");
            sword = new SwordMagic();
        }
    }

    void OnMove(InputValue value){
        if(!isDead && !isOccupied)
        {
            moveInput = value.Get<Vector2>();
            //If the player moves, set the animation to the correct direction
            if(moveInput != Vector2.zero){
                animator.SetFloat("XInput",moveInput.x);
                animator.SetFloat("YInput",moveInput.y);
                //change the direction of the reach hitbox to...
                if(moveInput.x > 0){        //right
                    gameObject.BroadcastMessage("PlayerDirection", 2);
                    playerDirection = new Vector3(0, 0, 0);
                }else if (moveInput.x < 0){ //left
                    gameObject.BroadcastMessage("PlayerDirection", 4);
                    playerDirection = new Vector3(0, 0, 180);
                }
                if(moveInput.y > 0){        //up
                    gameObject.BroadcastMessage("PlayerDirection", 1);
                    playerDirection = new Vector3(0, 0, 90);
                }else if (moveInput.y < 0){ //down
                    gameObject.BroadcastMessage("PlayerDirection", 3);
                    playerDirection = new Vector3(0, 0, 270);
                }
            }
        }
    }

    void OnRun(){
        if(!isDead && !isOccupied){
            Debug.Log("switched to basic sword");
            sword = new SwordBasic();
        }
    }
    
    void OnSword(){
        if(!isDead && !isOccupied){
            //tell the ReachHitbox that a sword attack is happening
            gameObject.BroadcastMessage("swordAttack", true);

            //tell the animator that a sword attack is happening
            animator.SetTrigger("swordAttack");
            sword.Attack();
        }
    }

    //this function listens for the dead message from IDamageable
    void Dead(){
        isDead = true;
        isOccupied = true;
    }

    void MagicSword(){
        //set player direction
        rotation = Quaternion.Euler(playerDirection);

        //spawn arrow, at player location, in same direction as player
        Instantiate(magic, transform.position, rotation);
    }

    void HookReturn(){
        // Debug.Log("Hook returned");
        isOccupied = false;
    }

    //to call from another object:
    /*
        if(GameObject.Find("Player") != null){
            GameObject gameObject = GameObject.Find("Player");
            gameObject.BroadcastMessage("IsOccupied");
        }
    */
    void IsOccupied(bool value){
        isOccupied = value;
    }
}