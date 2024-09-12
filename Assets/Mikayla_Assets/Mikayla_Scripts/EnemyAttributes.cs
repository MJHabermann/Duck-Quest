using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemyAttributes : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] protected int armor;
    [SerializeField] protected int speed;
    void Start(){
        //GameObject spawner = GameObject.Find("enemySpawner");
    }
    public void enemyInit(int hp, int dmg, int arm, int spd, int diff)
    {
        health = hp * diff;
        damage = dmg * diff;
        armor = arm * diff;
        speed = spd;
    }
        
}
