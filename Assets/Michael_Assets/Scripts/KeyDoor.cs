using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : Door
{
    public void UnlockWithKey()
    {
        isOpened = true;
        UpdateDoorSprite();
    }
}