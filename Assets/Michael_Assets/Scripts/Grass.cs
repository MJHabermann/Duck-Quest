using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Sword"))
        {
            Destroy(gameObject);
            Debug.Log("destroyed Grass Block");
        }
    }
}
