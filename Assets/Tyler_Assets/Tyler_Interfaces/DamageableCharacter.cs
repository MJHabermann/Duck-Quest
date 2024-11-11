using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    public bool disableSimulation = false;
    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;
    bool isAlive = true;
    public GameObject hud;
    public float Health{
        set{
            if(value < _health){
                animator.SetTrigger("hit");
            }

            _health = value;

            if (_health <= 0){
                animator.SetBool("isAlive", false);
                disableSimulation = true;
                Targetable = false;
                gameObject.BroadcastMessage("Dead");
                hud = GameObject.Find("PlayerHUD");
                hud.BroadcastMessage("Dead");
            }
        }
        get{
            return _health;
        }
    }

    public bool Targetable {
        get{
            return _targetable;
        }
        set{
            _targetable = value;
            if(disableSimulation){
                rb.simulated = false;
            }
            
            physicsCollider.enabled = value;
        }
    }
    public float _health = 3;
    public bool _targetable = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }

    public void OnHit(float damage, Vector2 knockback){
        Health -= damage;
        rb.AddForce(knockback);
        Debug.Log("Force: "+knockback);
    }

    public void OnHit(float damage){
        Health -= damage;
    }

    public void OnObjectDestroyed(){
        Destroy(gameObject);
    }
}
