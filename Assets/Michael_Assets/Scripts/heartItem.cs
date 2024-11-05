using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //add function to increase player health
            Debug.Log("collected heart");
            Destroy(gameObject);
        }
    }
}
