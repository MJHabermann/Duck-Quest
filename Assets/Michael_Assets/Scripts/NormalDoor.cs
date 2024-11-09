using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoor : Door
{
    public GameObject enemyPrefab;
    public Transform[] enemySpawnPoints;
    public GameObject rewardPrefab;
    public Transform rewardSpawnPoint;
    private bool inRoom = false;
    private static List<GameObject> spawnedEnemies = new List<GameObject>();
    private static bool isRoomCleared = false;
    private bool roomExplored = false;
    public override void Start()
    {
        base.Start();
        roomExplored = false;
        inRoom = false;
        isOpened = true;
    }

    public override void Update()
    {
        base.Update();
        if(inRoom)
        {
            CheckEnemiesCleared();
        }
        UpdateDoorSprite();
    }
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
    private void SpawnEnemies()
    {
        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
        isRoomCleared = false;
        isOpened = false;
        UpdateDoorSprite();
    }

    private void CheckEnemiesCleared()
    {
        spawnedEnemies.RemoveAll(enemy => enemy == null);
        if (spawnedEnemies.Count == 0 && !isRoomCleared)
        {
            //UpdateDoorSprite();
            //dropRewardOnClear = true;
            if (rewardPrefab != null && rewardSpawnPoint != null)
            {
                Debug.Log("dropping reward");
                Instantiate(rewardPrefab, rewardSpawnPoint.position, Quaternion.identity);
            }
            isRoomCleared = true;
            isOpened = true;
        }
    }


}