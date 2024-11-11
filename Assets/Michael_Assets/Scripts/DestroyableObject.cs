using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    //This is my static binding
    public virtual void DestroyObject()
    {
        Destroy(gameObject);
        Debug.Log("Destroyed Some Object");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            DestroyObject();
        }
    }
}
