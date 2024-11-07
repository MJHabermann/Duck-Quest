using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombDamage = 10f;
    public float knockbackForce = 1000f;
    public float timeToLive = 7f;
    public float timeBeforeDetonation = 5f;
    public AudioSource bombSound;
    private float timeSinceSpawned = 0f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool playedSound = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
        animator = GetComponent<Animator>();
        animator.SetTrigger("Fuse");
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawned += Time.deltaTime;
        if(timeSinceSpawned > timeBeforeDetonation){
            rb.simulated = true;
            if(!playedSound){
                playSound();
            }
            animator.SetTrigger("Explosion");
        }
        if(timeSinceSpawned > timeBeforeDetonation + 1){
            rb.simulated = false;
        }
        if(timeSinceSpawned > timeToLive){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        string tag = collider.gameObject.tag;
        Debug.Log(tag);
        if(tag == "Enemy"){
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(bombDamage);
            Debug.Log("Bomb deals " + bombDamage + " to enemy");
        }
        if(tag == "Player")
        {
            IDamageable player = collider.gameObject.GetComponent<IDamageable>();
            if(player != null){
                player.OnHit(bombDamage, transform.right * knockbackForce);
            }
        }
    }
    
    void playSound(){
        bombSound.Play();
        playedSound = true;
    }
}
