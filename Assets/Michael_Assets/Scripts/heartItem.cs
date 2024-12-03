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
            Player player = other.GetComponent<Player>();
            player.increaseHealth(1);
            Debug.Log("collected heart");
            Destroy(gameObject);
        }
    }
}
