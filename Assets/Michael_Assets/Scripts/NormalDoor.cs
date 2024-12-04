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
        //If the camera is panning because the player collided with the door
        if (isPanning)
        {
            //Start moving the camera and player position
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, panSpeed * Time.deltaTime);
            player.transform.position = Vector3.Lerp(player.transform.position, playerTargetPosition, panSpeed * Time.deltaTime);
            //This is what happens when the camera has moved up enough, signfying we have stopped moving
            if (Vector3.Distance(mainCamera.transform.position, targetPosition) < 0.01f && Vector3.Distance(player.transform.position, playerTargetPosition) < 0.01f)
            {
                mainCamera.transform.position = targetPosition;
                player.transform.position = playerTargetPosition;
                isPanning = false;
                isTransitioning = false;
                playerSpriteRenderer.enabled = true;
                if (!roomExplored)
                {
                    SpawnEnemies();
                    roomExplored = true;
                }
            }
        }
        else if (inRoom)
        {
            CheckEnemiesCleared();
        }
    }
    //Starts panning similarly 
    public override void StartPanning()
    {
        base.StartPanning();

        inRoom = !inRoom;
    }
    //Spawns all of the enemies
    // private void SpawnEnemies()
    // {
    //     //Spawns all of the enemies at the specific spawn points
    //     foreach (Transform spawnPoint in enemySpawnPoints)
    //     {
    //         GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    //     }
    //     isRoomCleared = false;//room is not cleared because we initially spawn the enemies
    //     Close();//Closes door
    // }
    private void SpawnEnemies()
    {
        // Spawns all of the enemies at the specific spawn points
        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemy == null)
            {
                Debug.LogError($"Failed to instantiate enemyPrefab at position {spawnPoint.position}");
                continue;
            }

            if (enemyComponent == null)
            {
                Debug.LogError($"The instantiated prefab does not have an Enemy component. Prefab: {enemyPrefab.name}");
                continue;
            }
            if (enemyComponent.EnemyName == "Goblin")
            {
                // Assign waypoints if the enemy is a Goblin
                Goblin goblin = enemy.GetComponent<Goblin>();
                if (goblin != null)
                {
                    // Create two waypoints within 2f of the spawn position
                    Transform waypoint1 = CreateWaypointNear(spawnPoint.position);
                    Transform waypoint2 = CreateWaypointNear(spawnPoint.position);

                    // Assign the waypoints to the Goblin
                    Transform[] waypointsForGoblin = new Transform[] { waypoint1, waypoint2 };
                    goblin.SetWaypoints(waypointsForGoblin);
                }
            }

            spawnedEnemies.Add(enemy);
        }

        isRoomCleared = false; // Room is not cleared because we initially spawn the enemies
        Close(); // Closes door
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

    // Method to create a waypoint near a given position
    private Transform CreateWaypointNear(Vector3 origin)
    {
        Vector3 offset = new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-3f, 3f), 0f);
        Vector3 waypointPosition = origin + offset;

        // Create a new GameObject to serve as the waypoint
        GameObject waypoint = new GameObject("Waypoint");
        Transform waypointTransform = waypoint.transform;
        waypointTransform.position = waypointPosition;

        return waypointTransform;
    }
}