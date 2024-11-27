using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombableDoor : Door
{
    //If one door gets bombed they all do by making this static 
    private static bool isBombed;
    //By default the door is not bombed
    public override void Start()
    {
        base.Start();
        isBombed = false;
    }
    //Checks if the door sprite updates
    public override void Update()
    {
        base.Update();
        UpdateDoorSprite();
    }
    //Updates the door sprite and is overwritten because the condition is now whether it is bombed rather than opened
    protected override void UpdateDoorSprite()
    {
        if (isBombed)
        {
            doorSpriteRenderer.sprite = openDoorSprite;
            doorSpriteRenderer.gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        else
        {
            doorSpriteRenderer.sprite = closedDoorSprite;
            doorSpriteRenderer.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
        switch (direction)
        {
            case PanDirection.Up:
                doorSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case PanDirection.Down:
                doorSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case PanDirection.Left:
                doorSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case PanDirection.Right:
                doorSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
        }
    }
    //Checks if the exploding bomb is touching the object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ExplodingBomb"))
        {
            isBombed = true;
            UpdateDoorSprite();
        }
        if (other.CompareTag("Player") && !isTransitioning && isOpened)
        {
            StartPanning();
        }
    }
}