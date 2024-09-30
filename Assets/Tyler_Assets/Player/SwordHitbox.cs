using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public Collider2D swordCollider;
    public float swordDamage = 1f;
    public float sowrdKnockback = 5f;
    public Vector3 faceUp = new Vector3(0, 0.33f, 0);
    public Vector3 faceRight = new Vector3(.25f, 0, 0);
    public Vector3 faceDown = new Vector3(0, -0.45f, 0);
    public Vector3 faceLeft = new Vector3(-.25f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        if(swordCollider == null){
            Debug.LogWarning("Sword Collider not set");
        }
    }

    //check for physics rigidbody and send hit damage to that GameObject
    void OnTriggerEnter2D(Collider2D collider){
        collider.SendMessage("OnHit", swordDamage);
        Debug.Log("Hit");
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
}
