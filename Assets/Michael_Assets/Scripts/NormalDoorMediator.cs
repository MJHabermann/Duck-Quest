using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mediator Pattern
public class NormalDoorMediator : MonoBehaviour
{
    [SerializeField] 
    private List<NormalDoor> doorsInRoom;
    //Looks for all of the doors and assignes a value based on the doors
    private void Start()
    {
        foreach (NormalDoor door in doorsInRoom)
        {
            door.DoorOpened += TrackOpenDoors;
            door.DoorClosed += TrackClosedDoors;
        }
    }
    public void TrackOpenDoors(NormalDoor door)
    {
        Debug.Log($"{door.name} opened. Room is free from enemies");
        foreach (NormalDoor openDoor in doorsInRoom) // opens all doors
        {
            openDoor.Open();
        }
    }

    public void TrackClosedDoors(NormalDoor door)
    {
        Debug.Log($"{door.name} closed. Room has enemies in it");
        foreach (NormalDoor closingDoor in doorsInRoom) // closes all doors
        {
            closingDoor.Close();
        }
    }

    private void OnDestroy()//When this object is destroyed it unassigns all of the doors
    {
        foreach (NormalDoor door in doorsInRoom)
        {
            door.DoorOpened -= TrackOpenDoors;
            door.DoorClosed -= TrackClosedDoors;
        }
    }
}
