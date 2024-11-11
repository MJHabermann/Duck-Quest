using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBasic : Sword
{
    //public int damage = 3;//This does not get used unless we start off as a SwordMagic class.
    override public void Attack(){
        Debug.Log("Basic Sword");
    }
}
