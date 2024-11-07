using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float hookDamage = 3f;
    // public float hookKnockback = 5f;
    public float moveSpeed = 10f;
    public float hookLength = 4.5f;
    public float hookGrowth = 0.23f;
    private Vector3 origin;
    
    public float timeToLive = 2.5f;
    public GameObject hookRope;
    private float timeSinceSpawned = 0f;
    private float halfwayTime = 0f;

    void Start(){
        origin = transform.position;
    }
    void Update()
    {
        //move
        transform.position += moveSpeed * transform.right * Time.deltaTime;

        //track time
        timeSinceSpawned += Time.deltaTime;

        //if we are growing in length
        if(moveSpeed > 0){
            hookRope.transform.localScale += (new Vector3(hookGrowth, 0, 0));
        }else if (moveSpeed < 0){
            hookRope.transform.localScale -= (new Vector3(hookGrowth, 0, 0));
        }

        //if hook has travelled to far in the...
        if(transform.position.x > origin.x + hookLength     //right direction
            ||transform.position.x < origin.x - hookLength  //left direction
            ||transform.position.y > origin.y + hookLength  //up direction
            ||transform.position.y < origin.y - hookLength) //down direction
        {
            ReturnToSender();
        }
        
        //returns to player without doing anything or stays for too long
        if(((timeSinceSpawned > 2 * halfwayTime) && (halfwayTime > 0)) 
            ||(timeSinceSpawned > timeToLive))
        {
            if(GameObject.Find("Player") != null){
                GameObject gameObject = GameObject.Find("Player");
                gameObject.BroadcastMessage("HookReturn");
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        //check what arrow collides with
        string tag = collider.gameObject.tag;
        if(tag == "Enemy")
        {
            ReturnToSender();
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(hookDamage);
        }
        else if(tag != null && tag != "Player" && tag != "Sword" && tag != "PlayerItem")
        {
            ReturnToSender();
            // gameObject.BroadcastMessage("HookHit");
        }
    }

    // this function changes the direction of travel for the hook
    // and updates the halfway time
    void ReturnToSender(){
        // Debug.Log("Changing direction");
        moveSpeed = moveSpeed * -1;
        halfwayTime = timeSinceSpawned;
    }
}