using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSwitch : PopUpParent
{
    public Collider2D switchCollider;
    public Animator animator;

    void Start()
    {
        BlueUp = true;
    }

    void OnTriggerEnter2D(Collider2D collider){
        //if player hits switch, call hit function
        string tag = collider.gameObject.tag;
        if(tag == "Sword" || tag == "PlayerItem"){
            Debug.Log("switch hit! Currently BlueUp is: "+BlueUp);
            hit();
            gameObject.BroadcastMessage("switchWasHit");
            //inform animator
        }
    }
    private void Update()
    {
        animator.SetBool("isBlue", BlueUp);
    }
    //change BlueUp to opposite function
    void hit(){
        BlueUp = !BlueUp;
        Debug.Log("switch BlueUp now: " + BlueUp);
    }
}
