using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : Door
{
    bool isLocked = true;
    
    private InventoryManager inventoryManager;
    public PlayerMemento p;
    public override void Start()
    {
        base.Start();
        isOpened = false;
    }
    public void UnlockDoor()
    {
        isLocked = false;
        isOpened = true;
        UpdateDoorSprite();
        Debug.Log("Door unlocked!");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (other.CompareTag("Player") && isLocked &&( player.getKeyAmount() >= 1))
        {
           player.setKeyCount(-1);
           UnlockDoor();
           UpdateDoorSprite();
        }
        else if (other.CompareTag("Player") && !isTransitioning && isOpened)
        {
            StartPanning();
        }
    }

    protected override void UpdateDoorSprite()
    {
        base.UpdateDoorSprite();
        if (isLocked)
        {
            doorSpriteRenderer.sprite = closedDoorSprite; // Show the closed door sprite if locked
            doorSpriteRenderer.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
        else
        {
            doorSpriteRenderer.sprite = openDoorSprite; // Show the open door sprite if unlocked
            doorSpriteRenderer.gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
    }
}