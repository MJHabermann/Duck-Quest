using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // public float spawnTime = 0.5f;
    // private float timeSinceSpawned = 0f;
    public float arrowDamage = 1f;
    public float arrowKnockback = 5f;
    public float moveSpeed = 10f;
    public float timeToLive = 5f;
    private float timeSinceSpawned = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * transform.right * Time.deltaTime;

        timeSinceSpawned += Time.deltaTime;
        if(timeSinceSpawned > timeToLive){
            Destroy(gameObject);
        }
        // timeSinceSpawned += timeSinceSpawned.deltaTime;
        // if(timeSinceSpawned >= spawnTime){
        //     Instantiate(Arrow, location, rotation);
        //     timeSinceSpawned = 0;
        // }
    }

    void OnTriggerEnter2D(Collider2D collider){
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();
        if(enemy != null){
                Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
                Vector3 direction = (Vector2) (parentPosition - collider.gameObject.transform.position).normalized;
                Vector3 knockback = direction * arrowKnockback;
                enemy.TakeDamage(arrowDamage);
                Destroy(gameObject);
        }
    }
}
