
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
    public GameObject memento;
    public GameObject hud;
    public Collider2D reachCollider;
    public AudioSource playerStep;
    public Quaternion rotation;
    public bool isDead = false;
    public bool isOccupied = false;
    // public float actionCooldown = .5f;
    // private float actionCooldownStart = 0f;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Vector2 moveInput;
    public Rigidbody2D rb;
    private Animator animator;
    /*Static binding. If this is used, then when sword variable is assigned 
    to new SwordMagic, the SwordMagic Attack() function will be called if it
    has the override prefix
    */
    private Sword sword = new Sword();

    /*Static binding. *Comment out portion of OnJump if used* If this is used,
    then it will always use the SwordMagic Attack() function regardless of whether
    override prefix is used or not.
    */
    // private SwordMagic sword = new SwordMagic(); 
    [SerializeField]
    private int bombCount;
    [SerializeField]
    private int arrowCount;
    private Vector3 playerDirection;
    private bool MS = false;
    // private PlayerMemento memento;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        reachCollider = reach.GetComponent<Collider2D>();
        hud = GameObject.Find("PlayerHUD");
        hud.BroadcastMessage("Load");
        memento = Instantiate(memento, new Vector3(0, 0, 0), Quaternion.identity);
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

    public void OnBomb(){
        if(!isDead && !isOccupied){
            //check if there is already a bomb in play
            if(GameObject.Find("Bomb(Clone)") == null){
                //create bomb if available
                if(bombCount > 0){
                Instantiate(bomb, transform.position, transform.rotation);
                bombCount--;
                GameObject gameObject = GameObject.Find("PlayerHUD");
                gameObject.BroadcastMessage("Save");
                Debug.Log("Bombs left: " + bombCount);
            }else{
                Debug.Log("No bombs");
            }
            }else{
                Debug.Log("Please wait, there is already a bomb!");
            }
        }
        
    }

    public void OnBow(){
        if(!isDead && !isOccupied){
            if(arrowCount > 0){
                //set player direction
                rotation = Quaternion.Euler(playerDirection);

                //spawn arrow, at player location, in same direction as player
                Instantiate(arrow, transform.position, rotation);
                arrowCount--;
                GameObject gameObject = GameObject.Find("PlayerHUD");
                gameObject.BroadcastMessage("Save");
                Debug.Log("Arrows left: " + arrowCount);
            }else{
                Debug.Log("No arrows");
            }
        }
    }

    public void OnHook(){
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

    public void OnInteract(){
        if(!isDead && !isOccupied){
            //tell the ReachHitbox to interact
            gameObject.BroadcastMessage("Interact");

            animator.SetTrigger("Interact");
        }
    }

    public void OnJump(){
        if(!isDead && !isOccupied){
            if(!MS){
                Debug.Log("Switched to magic sword");
                sword = new SwordMagic();
                MS = true;
            }else{
                Debug.Log("Switched to sword");
                sword = new Sword();
                MS = false;
            }
        }
    }

    public void OnMove(InputValue value){
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

    public void OnRun(){
        if(!isDead && !isOccupied){
            bombCount += 10;
            arrowCount += 10;
            // Debug.Log("switched to basic sword");
            // sword = new SwordBasic();
        }
    }
    
    public void OnSword(){
        if(!isDead && !isOccupied){
            //tell the ReachHitbox that a sword attack is happening
            gameObject.BroadcastMessage("swordAttack", true);
            //tell the animator that a sword attack is happening
            animator.SetTrigger("swordAttack");
            sword.Attack();
        }
    }

    //this function listens for the dead message from IDamageable
    public void Dead(){
        isDead = true;
        isOccupied = true;
    }

    public void MagicSword(){
        //set player direction
        rotation = Quaternion.Euler(playerDirection);

        //spawn arrow, at player location, in same direction as player
        Instantiate(magic, transform.position, rotation);
    }

    public void HookReturn(){
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
    public void IsOccupied(bool value){
        isOccupied = value;
    }
    public int getBombCount(){
        return bombCount;
    }
    public int getArrowCount(){
        return arrowCount;
    }
    public void setBombCount(int b){
        bombCount = b;
    }
    public void setArrowCount(int a){
        arrowCount = a;
    }
    public PlayerMemento createMemento(){
        //create a new memento and delete the old one
        GameObject newMemento = Instantiate(memento, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject oldMemento = memento;
        memento = newMemento;
        Destroy(oldMemento);
        memento.name = "PlayerMomento";

        //find the newly made memento and return it to the caretaker
        PlayerMemento playerMemento = FindObjectOfType<PlayerMemento>();
        playerMemento.Init(bombCount, arrowCount);
        return playerMemento;
    }
}