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
        playerMemento.restore(player);
    }

    void FixedUpdate(){
        
        if(player == null){
            player = FindObjectOfType<Player>();
            playerMemento.restore(player);
        }
        if(playerMemento == null){
            playerMemento = player.createMemento();
        }
    }
    // //when asked for memento, retrieve one from the player
    // PlayerMemento GetMemento(Player player, PlayerMemento memento){
        
    //     return player.createMemento(memento);
    // }

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
