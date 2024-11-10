using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombableDoor : Door
{
    private static bool isBombed;
    public override void Start()
    {
        base.Start();
        isBombed = false;
    }
    public override void Update()
    {
        base.Update();
        UpdateDoorSprite();
    }
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