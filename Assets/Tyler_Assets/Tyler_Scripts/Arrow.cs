using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float arrowDamage = 3f;
    public float arrowKnockback = 5f;
    public float moveSpeed = 10f;
    public float timeToLive = 5f;
    private float timeSinceSpawned = 0f;

    void Update()
    {
        //move
        transform.position += moveSpeed * transform.right * Time.deltaTime;

        //update active time
        timeSinceSpawned += Time.deltaTime;
        if(timeSinceSpawned > timeToLive){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        //check what arrow collides with
        string tag = collider.gameObject.tag;
        if(tag == "Enemy"){
            // gameObject.BroadcastMessage("ArrowHit");
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector3 direction = (Vector2) (parentPosition - collider.gameObject.transform.position).normalized;
            Vector3 knockback = direction * arrowKnockback;
            enemy.TakeDamage(arrowDamage);
            Destroy(gameObject);
        }else if(tag != null && tag != "Player" && tag != "Sword" && tag != "PlayerItem"){
            // gameObject.BroadcastMessage("ArrowHit");
            Destroy(gameObject);
        }
    }
}
