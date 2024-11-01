using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float timeToLive = 5f;
    private float timeSinceSpawned = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawned += Time.deltaTime;
        if(timeSinceSpawned > timeToLive){
            Destroy(gameObject);
        }
    }
}
