using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Slime : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }
    void CheckDistance(){
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius
        && Vector3.Distance(target.position, transform.position) > attackRadius){
            transform.position = Vector3.MoveTowards(
                transform.position, target.position, speed * Time.deltaTime);
        }
    }

        // Spawn method to create a new Slime instance
    public Slime Spawn(GameObject slimePrefab, Vector3 position, Quaternion rotation)
    {
        // Instantiate the Slime prefab
        GameObject slimeInstance = Instantiate(slimePrefab, position, rotation);
        return slimeInstance.GetComponent<Slime>(); // Return the Slime component from the instantiated object
    }
}
