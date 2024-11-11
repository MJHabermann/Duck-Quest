using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Subclass of the door Object
public class NormalDoor : Door
{
    //These are changable in the inspector
    public GameObject enemyPrefab;
    public Transform[] enemySpawnPoints;
    public GameObject rewardPrefab;
    public Transform rewardSpawnPoint;
    //These communicate with the Mediator for this door object
    public event Action<NormalDoor> DoorOpened;
    public event Action<NormalDoor> DoorClosed;
    //private variables that only this class should be able to change
    private bool inRoom = false;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private bool isRoomCleared = false;
    private bool roomExplored = false;
    //We want the same start but signify the room has not been explored yet
    public override void Start()
    {
        base.Start();
        roomExplored = false;
        inRoom = false;
        isOpened = true;
    }
    //Checks if we are in the room yet
    public override void Update()
    {
        base.Update();
        if(inRoom)
        {
            CheckEnemiesCleared();
        }
    }
    //Starts panning similarly 
    public override void StartPanning()
    {
        base.StartPanning();
        if(!roomExplored)
        {
            SpawnEnemies();
            roomExplored = true;
        }
        inRoom = !inRoom;
    }
    //Spawns all of the enemies
    private void SpawnEnemies()
    {
        //Spawns all of the enemies at the specific spawn points
        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
        isRoomCleared = false;//room is not cleared because we initially spawn the enemies
        Close();//Closes door
    }
    //This checks if the enemies are cleared in the room
    private void CheckEnemiesCleared()
    {
        
        spawnedEnemies.RemoveAll(enemy => enemy == null); //Removes all of the enemies that have been eliminated from the list of spawned enemies
        //If the list is 0 and the room has not been cleared yet
        if (spawnedEnemies.Count == 0 && !isRoomCleared)
        {
            //Drop a reward if the user passes one to the object
            if (rewardPrefab != null && rewardSpawnPoint != null)
            {
                Debug.Log("dropping reward");
                Instantiate(rewardPrefab, rewardSpawnPoint.position, Quaternion.identity);
            }
            
            isRoomCleared = true; //Make the room cleared so we do not spawn more enemies
            Open(); //Open the Door
        }
    }
    //Opens the Door and communicates with the Mediator
    public void Open()
    {
        if (!isOpened)
        {
            isOpened = true;
            UpdateDoorSprite();
            DoorOpened?.Invoke(this);
        }
    }
    //Closes the Door and communicates with the Mediator
    public void Close()
    {
        if (isOpened)
        {
            isOpened = false;
            UpdateDoorSprite();
            DoorClosed?.Invoke(this);
        }
    }
}