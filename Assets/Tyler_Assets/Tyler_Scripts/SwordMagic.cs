using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMagic : Sword
{
    //public int damage = 10;//This does not get used unless we start off as a SwordMagic class.

    // public void Attack(){   //Dynamic binding method for attack. Refers to parent, Sword
    override public void Attack(){ //Dynamic binding method for attack. Overwrites Attack from parent, Sword
        Debug.Log("Magic Sword");
        if(GameObject.Find("Player") != null){
            GameObject gameObject = GameObject.Find("Player");
            gameObject.BroadcastMessage("MagicSword");
        }
    }
}
