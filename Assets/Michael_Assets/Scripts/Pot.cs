using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : DestroyableObject
{
    public GameObject possibleDrop1;
    public GameObject possibleDrop2;
    public GameObject possibleDrop3;

    //This will be my Dynamic binding
    public override void DestroyObject()
    {
        
        Debug.Log("Destroyed Pot");
        Vector3 grassPosition = transform.position;//Finds the grasses position
        Debug.Log(grassPosition.x + " " + grassPosition.y + " " + grassPosition.z);
        //This is how we know it will drop something
        int randomChance = Random.Range(1, 6);
        if(randomChance == 1)
        {
            if (possibleDrop1 != null)
            {

                Instantiate(possibleDrop1, grassPosition, Quaternion.identity); // Spawns drop
                Debug.Log("Item Dropped");
            }
            else
            {
                Debug.LogWarning("Item is not assigned to object");
            }
        }
        else if (randomChance == 2)
        {
            if (possibleDrop2 != null)
            {

                Instantiate(possibleDrop2, grassPosition, Quaternion.identity); // Spawns drop
                Debug.Log("Item Dropped");
            }
            else
            {
                Debug.LogWarning("Item is not assigned to object");
            }
        }
        else if (randomChance == 3)
        {
            if (possibleDrop3 != null)
            {

                Instantiate(possibleDrop3, grassPosition, Quaternion.identity); // Spawns drop
                Debug.Log("Item Dropped");
            }
            else
            {
                Debug.LogWarning("Item is not assigned to object");
            }
        }
        Destroy(gameObject);//Destroys the grass
    }

}
