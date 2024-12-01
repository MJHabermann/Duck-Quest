using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSwitch : MonoBehaviour
{
    public bool isBlue = true;
    public Collider2D switchCollider;
    public Animator animator;

    void Start()
    {
        isBlue = true;
    }

    void OnTriggerEnter2D(Collider2D collider){
        //if player hits switch, call hit function
        string tag = collider.gameObject.tag;
        if(tag == "Sword" || tag == "PlayerItem"){
            Debug.Log("switch hit! Currently isBlue is: "+isBlue);
            hit();
            gameObject.BroadcastMessage("switchWasHit");
            //inform animator
            animator.SetBool("isBlue", isBlue);
        }

    }

    //change isBlue to opposite function
    void hit(){
        isBlue = !isBlue;
        Debug.Log("switch isblue now: " + isBlue);
    }
}
