using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject: MonoBehaviour
{
    // Start is called before the first frame update
  public virtual void DestroyObject()
    {
        Destroy(gameObject);
        Debug.Log("Destroyed Some Object");
    }
}


public class Grass : DestroyableObject
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            DestroyObject();
        }
    }
    public GameObject drops;
    public override void DestroyObject()
    {
        
        Debug.Log("Destroyed Grass Block");
        Vector3 grassPosition = transform.position;
        Debug.Log(grassPosition.x + " " + grassPosition.y + " " + grassPosition.z);
        if (drops != null) 
        {
            Instantiate(drops, grassPosition, Quaternion.identity);
            Debug.Log("Item Dropped");
        }
        else
        {
            Debug.LogWarning("Item is not assigned to object");
        }
        Destroy(gameObject);
    }

}