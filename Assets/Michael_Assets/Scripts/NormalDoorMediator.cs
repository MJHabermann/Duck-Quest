using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoorMediator : MonoBehaviour
{
    [SerializeField] 
    private List<NormalDoor> doorsInRoom;

    private void Start()
    {
        foreach (NormalDoor door in doorsInRoom)
        {
            door.DoorOpened += TrackOpenDoors;
            door.DoorClosed+= TrackClosedDoors;
        }
    }
    public void TrackOpenDoors(NormalDoor door)
    {
        Debug.Log($"{door.name} opened. Room is free from enemies");
        foreach (NormalDoor openDoor in doorsInRoom)
        {
            openDoor.Open();
        }
    }

    public void TrackClosedDoors(NormalDoor door)
    {
        Debug.Log($"{door.name} closed. Room has enemies in it");
        foreach (NormalDoor closingDoor in doorsInRoom)
        {
            closingDoor.Close();
        }
    }

    private void OnDestroy()
    {
        foreach (NormalDoor door in doorsInRoom)
        {
            door.DoorOpened -= TrackOpenDoors;
            door.DoorClosed -= TrackClosedDoors;
        }
    }
}
