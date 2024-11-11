using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCaretaker : MonoBehaviour
{
    public Player player;
    public PlayerMemento playerMemento;
    void Start()
    {
        //if no referenced player, find the player
        if(player == null){
            player = FindObjectOfType<Player>();
        }
        //if no referenced memento, make a new memento and reference it
        if(playerMemento == null){
            Instantiate(playerMemento, new Vector3(0, 0, 0), Quaternion.identity);
            playerMemento = FindObjectOfType<PlayerMemento>();
        }
        //restore player to memento status
        playerMemento.restore(player);
    }

    void FixedUpdate(){
        //if no player, find a player and restore status
        if(player == null){
            player = FindObjectOfType<Player>();
            playerMemento.restore(player);
        }
        //if no memento, create one
        if(playerMemento == null){
            playerMemento = player.createMemento();
        }
    }

    //when asked to set memento, save the memento for future use
    void requestMemento(){
        playerMemento = player.createMemento();
    }

    void Save(){
        Debug.Log("Saving player");
        requestMemento();
    }

    void Load(){
        Debug.Log("Loading player");
        playerMemento.restore(player);
    }
}
