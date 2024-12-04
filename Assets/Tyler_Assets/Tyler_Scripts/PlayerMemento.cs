using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMemento : MonoBehaviour
{
    [SerializeField]
    private int bombCount;
    [SerializeField]
    private int arrowCount;
    [SerializeField]
    private float health;
    [SerializeField]
    private int keyCount;
    public void Init(int b, int a, float h,int k){
        bombCount = b;
        arrowCount = a;
        health = h;
        keyCount = k;
    }
    public void restore(Player player){
        if(player != null){
            player.setBombCount(bombCount);
            player.setArrowCount(arrowCount);
            player.setKeyCount(keyCount);
            IDamageable playerHealth = player.GetComponent<IDamageable>();
            playerHealth.Health = health;
        }

    }
    public int getBombCount(){
        return bombCount;
    }
    public int getArrowCount(){
        return arrowCount;
    }

    public float getHealth(){
        return health;
    }

    public int getKeys()
    {
        return keyCount;
    }
}
