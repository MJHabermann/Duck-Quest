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

    override public void Attack(){
        Debug.Log("Magic Sword");
        if(GameObject.Find("Player") != null){
            GameObject gameObject = GameObject.Find("Player");
            gameObject.BroadcastMessage("MagicSword");
        }
    }
}
