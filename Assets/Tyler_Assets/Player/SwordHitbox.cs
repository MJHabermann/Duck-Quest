using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public Collider2D swordCollider;
    public float swordDamage = 1f;
    public float swordKnockback = 5f;
    public Vector3 faceUp = new Vector3(0, 0.33f, 0);
    public Vector3 faceRight = new Vector3(.25f, 0, 0);
    public Vector3 faceDown = new Vector3(0, -0.45f, 0);
    public Vector3 faceLeft = new Vector3(-.25f, 0, 0);
    private Animator animator;

    public AudioSource swordSound;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if(swordCollider == null){
            Debug.LogWarning("Sword Collider not set");
        }
    }

    //check for physics rigidbody and send hit damage to that GameObject
    void OnTriggerEnter2D(Collider2D collider){
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();
        // IDamageable damageableObject = (IDamageable) collider.GetComponent<IDamageable>();
        //if(damageableObject != null){
        if(enemy != null){
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector2 direction = (Vector2) (parentPosition - collider.gameObject.transform.position).normalized;
            Vector2 knockback = direction * swordKnockback;
            // collider.SendMessage("OnHit", swordDamage);
            //damageableObject.OnHit(swordDamage, knockback);
            enemy.TakeDamage(swordDamage);
        }else{
            Debug.LogWarning("Collider does not implement IDamageable");
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
        Debug.Log("Sword hit");
    }

    void swordAttack(bool attack){

        if(attack){
            animator.SetTrigger("swordAttack");
            swordSound.Play();
        }
        
    }

    
}
