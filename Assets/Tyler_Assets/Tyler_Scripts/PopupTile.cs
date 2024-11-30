using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTile : MonoBehaviour
{
    public bool isBlue = true;
    public bool isUp = true;
    public PopupSwitch popupSwitch;
    public Collider2D tileCollider;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isBlue", isBlue);
        //set default status to up 
        if(isBlue){
            isUp = true;
            animator.SetBool("isUp", isUp);
        }else{
            isUp = false;
            animator.SetBool("isUp", isUp);
        }
        // tileCollider.enabled = isUp;
    }

    void switchWasHit(){
        Debug.Log("tile told by parent");
        isUp = !isUp;
        animator.SetBool("isUp", isUp);
        // tileCollider.enabled = isUp;
    }
}
