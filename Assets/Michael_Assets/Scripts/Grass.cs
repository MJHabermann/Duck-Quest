using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Grass : DestroyableObject
{
    public GameObject drops;
    //This will be my Dynamic binding
    public override void DestroyObject()
    {
        
        Debug.Log("Destroyed Grass Block");
        Vector3 grassPosition = transform.position;//Finds the grasses position
        Debug.Log(grassPosition.x + " " + grassPosition.y + " " + grassPosition.z);
        //This is how we know it will drop something
        if (drops != null)
        {
            Instantiate(drops, grassPosition, Quaternion.identity); // Spawns drop
            Debug.Log("Item Dropped");
        }
        else
        {
            Debug.LogWarning("Item is not assigned to object");
        }
        Destroy(gameObject);//Destroys the grass
    }

}