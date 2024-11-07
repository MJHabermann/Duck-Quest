using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachHitbox : MonoBehaviour
{
    public Collider2D reachCollider;
    public float swordDamage = 1f;
    public float swordKnockback = 5f;
    public Vector3 faceUp = new Vector3(0, 0.15f, 0);
    public Vector3 faceRight = new Vector3(.25f, -.15f, 0);
    public Vector3 faceDown = new Vector3(0, -0.45f, 0);
    public Vector3 faceLeft = new Vector3(-.25f, -.15f, 0);
    public AudioSource swordSound;
    private Animator animator;
    private string triggerCode;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if(reachCollider == null){
            Debug.LogWarning("Reach Collider not set");
        }
    }

    //check for physics rigidbody and send hit damage to that GameObject
    void OnTriggerEnter2D(Collider2D collider){
        if(triggerCode == "Attack"){
            string tag = collider.gameObject.tag;
            if(tag == "Enemy"){
                Enemy enemy = collider.gameObject.GetComponent<Enemy>();
                if(enemy != null){
                    Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
                    Vector2 direction = (Vector2) (parentPosition - collider.gameObject.transform.position).normalized;
                    Vector2 knockback = direction * swordKnockback;
                    // collider.SendMessage("OnHit", swordDamage);
                    enemy.TakeDamage(swordDamage);
                }
            }

        }else if (triggerCode == "Interact"){
            string tag = collider.gameObject.tag;
            if(tag == "Chest"){
                Debug.Log("Attempted to open");
                Chest chest = collider.gameObject.GetComponent<Chest>();
                if(chest != null){
                    chest.Open();
                }
            }else if(tag == "Talkable"){
                Debug.Log("Attempted to talk with");
            }else if(tag == "Interactive"){
                Debug.Log("Attempted to interact with");
            }
        }
        
    }

    //direction is clockwise, with the start at up (1)
    //up is 1, right is 2, down is 3, left is 4
    void PlayerDirection(int direction){
        if(direction == 1){
            gameObject.transform.localPosition = faceUp;
        }else if (direction == 2){
            gameObject.transform.localPosition = faceRight;
        }else if (direction == 4){
            gameObject.transform.localPosition = faceLeft;
        }else{//down
            gameObject.transform.localPosition = faceDown;
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        Debug.Log("Reach hit");
    }

    public void swordAttack(bool attack){
        if(attack){
            triggerCode = "Attack";
            animator.SetTrigger("swordAttack");
            swordSound.Play();
        }
    }

    void Interact(){
        triggerCode = "Interact";
        animator.SetTrigger("Interact");
    }
}
