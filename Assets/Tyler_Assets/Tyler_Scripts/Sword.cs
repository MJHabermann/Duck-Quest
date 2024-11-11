using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword
{
    public int damage = 2;
    virtual public void Attack(){
        Debug.Log("Sword");
    }
}
