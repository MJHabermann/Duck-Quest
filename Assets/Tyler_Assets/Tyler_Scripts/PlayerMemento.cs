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
        player.setBombCount(bombCount);
        player.setArrowCount(arrowCount);
    }
    public int getBombCount(){
        return bombCount;
    }
    public int getArrowCount(){
        return arrowCount;
    }
    // private void setBombCount(int b){
    //     bombCount = b;
    // }
    // private void setArrowCount(int a){
    //     arrowCount = a;
    // }
}
