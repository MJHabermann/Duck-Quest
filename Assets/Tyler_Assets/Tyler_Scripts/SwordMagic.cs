using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMagic : Sword
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void Attack(){   //Static bound method for attack. Refers to parent, Sword
    override public void Attack(){ //Dynamic bound method for attack. Overwrites Attack from parent, Sword
        Debug.Log("Magic Sword");
        if(GameObject.Find("Player") != null){
            GameObject gameObject = GameObject.Find("Player");
            gameObject.BroadcastMessage("MagicSword");
        }
    }
}
