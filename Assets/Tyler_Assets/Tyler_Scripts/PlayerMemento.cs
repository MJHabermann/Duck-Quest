using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMemento : MonoBehaviour
{
    [SerializeField]
    private int bombCount;
    [SerializeField]
    private int arrowCount;
    public void Init(int b, int a){
        bombCount = b;
        arrowCount = a;
    }
    public void restore(Player player){
        if(player != null){
            player.setBombCount(bombCount);
            player.setArrowCount(arrowCount);
        }

    }
    public int getBombCount(){
        return bombCount;
    }
    public int getArrowCount(){
        return arrowCount;
    }
}
